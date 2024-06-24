using System;
using System.Collections;
using System.Text;
using IBM.WMQ;
using Newtonsoft.Json;
using WebT.Common;

namespace WebT
{
    public class MQHelper
    {
        private static string QMGR = "QM1";
        private static string REQUEST_QUEUE = "TESTMMI";
        private static string RESPONSE_QUEUE = "TESTMMO";
        private static string HOSTNAME = "luciuslu.kmdns.net";
        private static int PORT = 41412;
        private static string CHANNEL = "DEV.ADMIN.SVRCONN";
        private static string USERNAME = "admin";
        private static string PASSWORD = "passw0rd";
        private static int TIMEOUT = 10000; // 超时时间设置为10秒

        [Obsolete]
        public TResult SendAndReceiveMessage(string transaction,string data)
        {
            TResult result = new TResult("", "", null);
            CommonFunc commonfunc = new CommonFunc() ;
            MQQueueManager queueManager = null;
            MQQueue requestQueue = null;
            MQQueue responseQueue = null;

            try
            {
                // 创建连接到远程队列管理器的属性
                Hashtable properties = new Hashtable
                {
                    { MQC.HOST_NAME_PROPERTY, HOSTNAME },
                    { MQC.PORT_PROPERTY, PORT },
                    { MQC.CHANNEL_PROPERTY, CHANNEL },
                    { MQC.USER_ID_PROPERTY, USERNAME },
                    { MQC.PASSWORD_PROPERTY, PASSWORD }
                };

                // 连接到队列管理器
                queueManager = new MQQueueManager(QMGR, properties);

                // 打开请求队列进行输出
                requestQueue = queueManager.AccessQueue(REQUEST_QUEUE, MQC.MQOO_OUTPUT);

                // 打开响应队列进行输入
                responseQueue = queueManager.AccessQueue(RESPONSE_QUEUE, MQC.MQOO_INPUT_AS_Q_DEF);

                var messageContent = new TMessage
                {
                    Transaction = JsonConvert.DeserializeObject<Transaction>(transaction),
                    Data = JsonConvert.DeserializeObject<dynamic>(data)
                };
                string message = JsonConvert.SerializeObject(messageContent, Formatting.None);
                MQMessage requestMessage = new MQMessage();

                requestMessage.WriteString(message);

                // 设置唯一标识ID
                string correlationId = Guid.NewGuid().ToString().ToUpper();
                byte[] correlationIdBytes = new byte[24];
                Array.Copy(Encoding.UTF8.GetBytes(correlationId), correlationIdBytes, 16); // 使用前16字节
                requestMessage.CorrelationId = correlationIdBytes;
                requestMessage.ReplyToQueueName = RESPONSE_QUEUE;

                // 创建消息描述符
                MQPutMessageOptions pmo = new MQPutMessageOptions();

                // 将消息放入请求队列
                requestQueue.Put(requestMessage, pmo);
                //Console.WriteLine("Message sent: " + message + " with Correlation ID: " + correlationId);
                commonfunc.LogJson(CommonDef.TX_SEND_TYPE, message);
                // 等待响应消息
                MQMessage responseMessage = new MQMessage();
                MQGetMessageOptions gmo = new MQGetMessageOptions
                {
                    MatchOptions = MQC.MQMO_MATCH_CORREL_ID,
                    WaitInterval = TIMEOUT,
                    Options = MQC.MQGMO_WAIT | MQC.MQGMO_FAIL_IF_QUIESCING
                };
                responseMessage.CorrelationId = correlationIdBytes;

                try
                {
                    responseQueue.Get(responseMessage, gmo);

                    // 读取响应消息
                    string response = responseMessage.ReadString(responseMessage.MessageLength);
                    commonfunc.LogJson(CommonDef.TX_RECV_TYPE, response);
                    TResult resRst = JsonConvert.DeserializeObject<TResult>(response);
                    // 设置返回结果
                    result.Result = resRst.Result;
                    result.Data = resRst.Data;
                    result.Message = resRst.Message;
                }
                catch (MQException mqe)
                {
                    if (mqe.ReasonCode == MQC.MQRC_NO_MSG_AVAILABLE)
                    {
                        Console.WriteLine("Timeout: No response received within the given timeout period.");
                        result.Result = "Timeout";
                        result.Message = "No response received within the given timeout period.";
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            catch (MQException mqe)
            {
                Console.WriteLine("MQException: " + mqe.Message);
                result.Result = "Error";
                result.Message = mqe.Message;
            }
            finally
            {
                // 关闭队列并断开与队列管理器的连接
                if (requestQueue != null)
                    requestQueue.Close();
                if (responseQueue != null)
                    responseQueue.Close();
                if (queueManager != null)
                    queueManager.Disconnect();
            }

            return result;
        }
    }
}
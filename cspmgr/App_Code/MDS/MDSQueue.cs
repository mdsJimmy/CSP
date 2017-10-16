using System;
using System.Messaging;
using System.Runtime.CompilerServices;

namespace MDS.MSMQ
{
	/// <summary>
	/// MDSQueue 提供處理發送及接收Message Queue的方法及屬性。
	/// </summary>
	public class MDSQueue
	{
		private delegate bool AsyncDelegate(object o);

		private string machineName;
		private string queueName;
        private MessageQueue MSMQueue;         //2015-01-21 modify
        private object lockObject = new object();



		/// <summary>
		/// MDSQueue建構函式，並且設定Message Queue路徑
		/// </summary>
		/// <param name="existingMachineName">MachineName</param>
		/// <param name="existingQueueName">QueueName</param>
		public MDSQueue(string existingMachineName, string existingQueueName)
		{
			machineName = existingMachineName;
			queueName = existingQueueName;
            if (!MessageQueue.Exists(existingMachineName + "\\" + existingQueueName))
            {
                MessageQueue.Create(existingMachineName + "\\" + existingQueueName);
                MSMQueue = new MessageQueue(existingMachineName + "\\" + existingQueueName);
                MSMQueue.SetPermissions("Everyone", MessageQueueAccessRights.FullControl);
            }
            else
            {
                MSMQueue = new MessageQueue(existingMachineName + "\\" + existingQueueName);
            }
		}

		/// <summary>
		/// 傳送Queue
		/// </summary>
		/// <param name="o">被傳送的物件</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
		public void SendMesageQueue(object o)
		{
            MSMQueue.Formatter = new XmlMessageFormatter(new Type[] { o.GetType() });
			AsyncDelegate dlgt = new AsyncDelegate(Send);
			IAsyncResult ar = dlgt.BeginInvoke(o, new AsyncCallback(MSMQCallbackMethod), dlgt);
		}

		/// <summary>
		/// 將可續列化的物件使用Message Queue傳送
		/// </summary>
		/// <param name="o">被傳送的物件</param>
		/// <param name="existingMachineName">MachineName</param>
		/// <param name="existingQueueName">QueueName</param>
		//public static void SendMesageQueue(object o, string existingMachineName, string existingQueueName)
        public  void SendMesageQueue(object o, string existingMachineName, string existingQueueName)
		{
			if(!MessageQueue.Exists(existingMachineName + "\\" + existingQueueName))
			{
				MessageQueue.Create(existingMachineName + "\\" + existingQueueName);
				MSMQueue = new MessageQueue(existingMachineName + "\\" + existingQueueName);
				MSMQueue.SetPermissions("Everyone", MessageQueueAccessRights.FullControl);
			}
			else
			{
				MSMQueue = new MessageQueue(existingMachineName + "\\" + existingQueueName);
			}
			MSMQueue.Formatter = new XmlMessageFormatter(new Type[] {o.GetType()});
			AsyncDelegate dlgt = new AsyncDelegate(Send);
			IAsyncResult ar = dlgt.BeginInvoke(o, new AsyncCallback(MSMQCallbackMethod), dlgt);
		}

        
		//private static bool Send(object o)
        private  bool Send(object o)
		{
			try
			{
                ///2015-01-21 modify
                lock (lockObject)
                {
                    MSMQueue.Send(o);
                }
				return true;
			}
			catch(Exception e)
			{
				throw(e);
			}
		}

		private static void MSMQCallbackMethod(IAsyncResult ar)
		{
			AsyncDelegate dlgt = (AsyncDelegate)ar.AsyncState;

			bool returnValue = dlgt.EndInvoke(ar);

			//傳送失敗
			if(!returnValue)
			{
				//do some thing here
			}
		}

		/// <summary>
		/// 主機名稱
		/// </summary>
		public string MachineName
		{
			get
			{
				return machineName;
			}
			set
			{
				machineName = value;
			}
		}

		/// <summary>
		/// Queue名稱
		/// </summary>
		public string QueueName
		{
			get
			{
				return queueName;
			}
			set
			{
				queueName = value;
			}
		}
	}
}

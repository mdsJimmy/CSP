using System;
using System.Messaging;
using System.Runtime.CompilerServices;

namespace MDS.MSMQ
{
	/// <summary>
	/// MDSQueue ���ѳB�z�o�e�α���Message Queue����k���ݩʡC
	/// </summary>
	public class MDSQueue
	{
		private delegate bool AsyncDelegate(object o);

		private string machineName;
		private string queueName;
        private MessageQueue MSMQueue;         //2015-01-21 modify
        private object lockObject = new object();



		/// <summary>
		/// MDSQueue�غc�禡�A�åB�]�wMessage Queue���|
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
		/// �ǰeQueue
		/// </summary>
		/// <param name="o">�Q�ǰe������</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
		public void SendMesageQueue(object o)
		{
            MSMQueue.Formatter = new XmlMessageFormatter(new Type[] { o.GetType() });
			AsyncDelegate dlgt = new AsyncDelegate(Send);
			IAsyncResult ar = dlgt.BeginInvoke(o, new AsyncCallback(MSMQCallbackMethod), dlgt);
		}

		/// <summary>
		/// �N�i��C�ƪ�����ϥ�Message Queue�ǰe
		/// </summary>
		/// <param name="o">�Q�ǰe������</param>
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

			//�ǰe����
			if(!returnValue)
			{
				//do some thing here
			}
		}

		/// <summary>
		/// �D���W��
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
		/// Queue�W��
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

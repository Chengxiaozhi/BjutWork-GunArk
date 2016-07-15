using System;
using System.Collections.Generic;
using System.Text;
using Model = Gunark.Model;

namespace MyUI.Utils
{
    public class MyObserver
    {

        private System system = null;

        public System System1
        {
            get { return system; }
            set { system = value; }
        }
        private TaskObserver taskObserver = null;

        public TaskObserver TaskObserver1
        {
            get { return taskObserver; }
            set { taskObserver = value; }
        }
        public MyObserver() 
        {  
            
        }
        public System getSystemInstance()
        {
            if (system == null)
                system = new System();
            return system;
        }
        public TaskObserver getTaskObserver(Model.task_info _task_info, List<string> _gun_position_info_id, Subject sub)
        {
            taskObserver = new TaskObserver(_task_info,_gun_position_info_id, sub);
            return taskObserver;
        }
        //通知者接口
        public interface Subject
        {
            void Attach(Observer observer);
            void Detach(Observer observer);
            void Notify();
            string SubjectState
            {
                get;
                set;
            }
        }

        public class System : Subject
        {
            //任务列表
            private IList<Observer> observers = new List<Observer>();
            private string action;

            //增加
            public void Attach(Observer observer)
            {
                observers.Add(observer);
            }

            //减少
            public void Detach(Observer observer)
            {
                observers.Remove(observer);
            }

            //通知
            public void Notify()
            {
                foreach (Observer o in observers)
                    o.Update();
            }

            //前台状态
            public string SubjectState
            {
                get { return action; }
                set { action = value; }
            }
        }

        //抽象观察者
        public abstract class Observer
        {
            protected Model.task_info task_info;
            protected List<string> gun_position_info_id;
            protected Subject sub;

            public Observer(Model.task_info _task_info, List<string> _gun_position_info_id , Subject sub)
            {
                this.task_info = _task_info;
                this.gun_position_info_id = _gun_position_info_id;
                this.sub = sub;
            }

            public abstract void Update();
        }

        //任务
        public class TaskObserver : Observer
        {
            //调用WebServie接口工具
            WebService.gunServices webService = SingleWebService.getWebService();
            public TaskObserver(Model.task_info _task_info, List<string> _gun_position_info_id, Subject sub)
                : base(_task_info,_gun_position_info_id, sub)
            {
            }

            public override void Update()
            {
                //Console.WriteLine("{0} {1} 关闭股票行情，继续工作！", sub.SubjectState, task_id);
                if (task_info.task_Status.Equals("5"))
                {
                    //调用WebService接口，更改枪位状态
                    try
                    {
                        for (int i = 0; i < gun_position_info_id.Count; i++)
                        {
                            webService.setGunNotOnPosition(task_info.UNIT_ID, task_info.GUNARK_ID, gun_position_info_id[i]);
                        }
                        //通知完成后从通知列表移除
                        new MyObserver().getSystemInstance().Detach(this);
                    }
                    catch(Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                   
                }
                else
                {
                    //调用WebService接口，更改枪位状态
                    try
                    {
                        for (int i = 0; i < gun_position_info_id.Count; i++)
                        {
                            webService.setGunOnPosition(task_info.UNIT_ID, task_info.GUNARK_ID, gun_position_info_id[i]);
                        }
                        //通知完成后从通知列表移除
                        new MyObserver().getSystemInstance().Detach(this);
                    }
                    catch(Exception e1)
                    {
                        Console.WriteLine(e1.Message);
                    }
                }
            }
        }

        
    }
}

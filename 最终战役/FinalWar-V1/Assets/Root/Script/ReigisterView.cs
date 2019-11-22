using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

/*
 *	
 *  
 *
 *	by Xuanyi
 *
 */

namespace MoleMole
{
    public class ReigisterContext : BaseContext
    {
        public ReigisterContext()
            : base(UIType.Reigister)
        {

        }
    }

    public class ReigisterView : AnimateView
    {
        //持有用户名和密码这两个输入框的对象
        [SerializeField]
        public InputField Username;
        [SerializeField]
        public InputField Password;
        [SerializeField]
        public InputField Confirm;

        //定义访问JSP登录表单的get方式访问路径
        //private string Url = "http://192.168.234.1:8080/Register.do?";
        private string Url = "http://47.93.195.255:8080/Register.do?";


        public override void OnEnter(BaseContext context)
        {
            base.OnEnter(context);
      
        }

        public override void OnExit(BaseContext context)
        {
            base.OnExit(context);
        }

        public override void OnPause(BaseContext context)
        {
            base.OnPause(context);
        }

        public override void OnResume(BaseContext context)
        {
            base.OnResume(context);
        }
 

        //当注册按钮被点击
        public void ReigisterButtonOnClick()
        {
            if (Username.text.Length == 0 || Password.text.Length == 0||Confirm.text.Length == 0)
            {
                PlayerPrefs.SetInt("SelectView", 1);
                Singleton<LandContextManager>.Instance.Push(new PopContext(),"账号、密码或确认栏不能为空");
                return;
            }
            //判断passwrod是否都由字母或数字组成
            string pattern = @"^[a-zA-Z0-9]*$";
            if (!System.Text.RegularExpressions.Regex.IsMatch(Password.text, pattern))
            {
                PlayerPrefs.SetInt("SelectView", 1);
                Singleton<LandContextManager>.Instance.Push(new PopContext(), "密码只能有字母和数字组成");
                return;
            }
            if (!Password.text.Equals(Confirm.text)){
                PlayerPrefs.SetInt("SelectView", 1);
                Singleton<LandContextManager>.Instance.Push(new PopContext(), "两次输入的密码不同");
                return;
            }
            //向服务器传递的参数
            string parameter = "";
            parameter += "UserName=" + Username.text + "&";
            parameter += "PassWord=" + Password.text;

            //开始传递
            StartCoroutine(login(Url + parameter));

        }

        //访问JSP服务器
        IEnumerator login(string path)
        {
            WWW www = new WWW(path);
            yield return www;
            //如果发生错误，打印这个错误
            if (www.error != null)
            {
                Debug.Log(www.error);
            }
            else
            {
                //如果服务器返回的是true
                if (www.text.Equals("true"))
                {
                    //注册成功
                    PlayerPrefs.SetInt("SelectView", 1);
                    Singleton<LandContextManager>.Instance.Push(new PopContext(), "注册成功！");
                }
                else
                {
                    //否则登录失败
                    PlayerPrefs.SetInt("SelectView", 1);
                    Singleton<LandContextManager>.Instance.Push(new PopContext(), "账号已经存在！");
                }
            }
        }


        //当返回按钮点击时
        public void BackButtonOnClick()
        {
            Singleton<LandContextManager>.Instance.Pop();
        }
    }
}

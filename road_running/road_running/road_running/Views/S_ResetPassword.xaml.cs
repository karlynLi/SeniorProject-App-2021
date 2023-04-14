﻿using Rg.Plugins.Popup.Services;
using road_running.Models;
using road_running.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace road_running.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class S_ResetPassword : ContentPage
    {
        public string email;
        public static List<Staff> ResetResult { get; set; }
        public S_ResetPassword(string mail)
        {
            InitializeComponent();
            email = mail;
        }
        private async void UpdatePass(object sender, EventArgs e)
        {
            if (pass1.Text == pass2.Text)
            {
                Staff update = new Staff()
                {
                    Email = email,
                    Password = pass2.Text
                };
                ResetResult = await SForgetPwdProvider.ResetAsync(update);
                Console.WriteLine(ResetResult[0].ans);
                if (ResetResult[0].ans == "yes")
                {
                    var myPopup = new DisPlayMessage("重設成功", ResetResult[0].ans, "確定");
                    await PopupNavigation.Instance.PushAsync(myPopup);
                    await myPopup.PopupClosedTask;
                    //await PopupNavigation.PushAsync(new DisPlayMessage("重設成功", ResetResult[0].ans));
                    await Navigation.PopToRootAsync();
                }
                else if (ResetResult[0].ans == "no")
                {
                    var myPopup = new DisPlayMessage("重設失敗", ResetResult[0].ans, "返回");
                    await PopupNavigation.Instance.PushAsync(myPopup);
                    await myPopup.PopupClosedTask;
                    //await PopupNavigation.PushAsync(new DisPlayMessage("重設失敗", ResetResult[0].ans));
                }
                else
                {
                    var myPopup = new DisPlayMessage("輸入不可為空", ResetResult[0].ans, "重新輸入");
                    await PopupNavigation.Instance.PushAsync(myPopup);
                    await myPopup.PopupClosedTask;
                    //await PopupNavigation.PushAsync(new DisPlayMessage("輸入不可為空", ResetResult[0].ans));
                }
            }
            else
            {
                var myPopup = new DisPlayMessage("錯誤", "兩次輸入的密碼不同!", "重新輸入");
                await PopupNavigation.Instance.PushAsync(myPopup);
                await myPopup.PopupClosedTask;
                // await PopupNavigation.PushAsync(new DisPlayMessage("錯誤", "兩次輸入的密碼不同!"));
            }
        }
    }
}
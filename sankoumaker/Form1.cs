using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

using System.Net.Http;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace sankoumaker
{
    public partial class Form1 : Form
    {
  
        private static readonly HttpClient client = new HttpClient();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

   
        private async void button16_Click(object sender, EventArgs e)
        {
          
            string url = urls.Text.Trim();

            if (string.IsNullOrEmpty(url) || !url.StartsWith("http"))
            {
                MessageBox.Show("有効なURLを入力してください。");
                return;
            }

            try
            {
            
                string html = await client.GetStringAsync(url);

               
                string tit = ExtractTitle(html);
                if (string.IsNullOrEmpty(tit))
                {
                    tit = "タイトル不明";
                }

               
                string num = "[" + numericUpDown1.Value + "]";
                string auth = textBox4.Text + " ";
                string auth2 = textBox4.Text;
                string appen = " (" + dateTimePicker1.Text + "アクセス)";
                string title = "“" + tit + "”";

                if (checkBox2.Checked == false)
                {
                   
                    if (auth == " ")
                    {
                        if (num == "[0]")
                            textBox5.Text = title + "\r\n" + "URL " + url + appen;
                        else
                            textBox5.Text = num + title + "\r\n" + "URL " + url + appen;
                    }
                    else
                    {
                        if (num == "[0]")
                            textBox5.Text = auth + title + "\r\n" + "URL " + url + appen;
                        else
                            textBox5.Text = num + auth + title + "\r\n" + "URL " + url + appen;
                    }
                }
                else
                {
                 
                    if (auth == " ")
                    {
                        if (num == "[0]")
                            textBox5.Text = title + "\r\n" + "URL " + url + appen;
                        else
                            textBox5.Text = num + title + "\r\n" + "URL " + url + appen;
                    }
                    else
                    {
                   
                        if (num == "[0]")
                            textBox5.Text = auth2 + "." + "(" + dateTimePicker1.Value.Year + ")." + title.Replace(auth, "") + "\r\n" + url;
                        else
                            textBox5.Text = num + auth2 + "." + "(" + dateTimePicker1.Value.Year + ")." + title.Replace(auth, "") + "\r\n" + url;
                    }
                }


                Clipboard.SetDataObject(textBox5.Text, true);
                MessageBox.Show("参考文献明示文を作成し、クリップボードにコピーしました!");

                this.Close(); 
            }
            catch (Exception ex)
            {
                MessageBox.Show("参考文献明示文の作成に失敗しました。URLが正しいか、またはインターネット接続を確認してください。\nエラー詳細: " + ex.Message);
            }
        }

      
        private string ExtractTitle(string html)
        {
        
            Match match = Regex.Match(html, @"<title[^>]*>(.*?)</title>", RegexOptions.IgnoreCase | RegexOptions.Singleline);
            if (match.Success)
            {
        
                return System.Net.WebUtility.HtmlDecode(match.Groups[1].Value.Trim());
            }
            return "";
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
                label9.Text = "構成(英語文献式): [番号(指定した場合)]著者名.(年データ).“概要”\r\nURL";
            else
                label9.Text = "構成(日本語文献式): [番号(指定した場合)] 著者名 \"タイトル\"\r\nURL \"URL\"(取得日時)";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("このツールでは高速にデータ取得を行える反面、動的なサーバーのデータは取り扱うことができません。\nタイトル情報が正しく取得されない場合はSankouMaker 2をご利用ください。");
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("SankouMaker\nCopyright (C) 2026 横茶横葉(YokochaYokoha)\nThis software is released under the MIT License.\nhttps://yokonoha.github.io/");
            Process.Start("https://yokonoha.github.io/");
        }
    }
}
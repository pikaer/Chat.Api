帮助文档
版本：2.18.0625.1125

A.配置说明
1. From。发送人邮箱地址。
2. DisplayName。发件人显示名称。
3. Username。发件人帐号。
4. Password。发件人密码。
5. Smtp。smtp服务器地址。
6. Port。smtp服务器端口。

B.调用说明
1. bool Send(string receivers, string subject, string body)
  功能：同步发送邮件
  入参：
     receivers: 收件人邮箱地址
     subject：邮件主题；
	 body：邮件内容；
  出参：是否已发送。仅表示是否执行了发送，不代表收件人是否成功接收

2. void SendAsync(string receivers, string subject, string body)
  功能：异步发送邮件
  入参：同Send
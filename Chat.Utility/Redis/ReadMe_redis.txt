帮助文档
版本：2.18.0625.1125

★请注意★
1. 当前版本不兼容所有1.0版本；
2. 配置有改动，请覆盖旧配置文件；
3. 不建议使用Redis缓存大容量数据，如有此类需求，建议使用ES或MongoDb进行存储；

A.配置说明
1. Connection。Redis连接配置字符串。
	Redis的连接字符串是以英文,分隔的配置项的组合，如：10.40.3.2:6379,allowadmin=true,defaultDatabase=0
	常见配置项：
	abortConnect={bool}:如果为true，Connect 没有服务器可用时将不会创建连接
	allowAdmin={bool}：启用被认为具有风险的一系列命令；
	connectRetry={int}：在初始 Connect 期间重复连接尝试的次数；
	connectTimeout={int}：连接操作的超时时间（ms）；
	defaultDatabase={int}：默认数据库索引, 从 0 到 databases - 1（0 到 Databases.Count -1）；
	keepAlive={int}：发送消息以帮助保持套接字活动的时间（秒）；
	name={string}：连接帐号；
	password={string}：密码；
	ssl={bool}：指定应使用SSL加密；
	更多配置项请参考：
	源码文档：https://github.com/StackExchange/StackExchange.Redis/blob/master/docs/Configuration.md
	中文译文：https://www.jianshu.com/p/0243277cd2f8

B.常用的调用方法
	计数器：
	1. double Increment(string key, double increments = 1, int? dbNo = null)
		功能：递增计数
		常用场景：文章浏览次数、抢单正向计数等
		入参：
		key：Redis Key
		increments：递增量，默认为1
		dbNo：Redis库号，一般为0~15
		出参：递增后的计数
	2. static double Decrement(string key, double decrements = 1, int? dbNo = null)
		功能：递增计数
		常用场景：抢单逆向计数等
		入参、出参同Increment

	String操作
	1. bool StringSet(string key, string value, TimeSpan? expiry = default(TimeSpan?), int? dbNo = null)
		功能：Key-Value形式存储string值
		常用场景：缓存小片段的字符型的数据
		入参：
		key：Redis Key
		value：数据值
		expiry：过期时间
		dbNo：Redis库号，一般为0~15
		出参：是否成功

	2. string StringGet(string key, int? dbNo = null)
		功能：获取Key-Value形式存储的string值
		常用场景：缓存小片段的字符型的数据
		入参：
		key：Redis Key
		dbNo：Redis库号，一般为0~15
		出参：缓存的String值

	3. bool Set<T>(string key, T value, TimeSpan? expiry = default(TimeSpan?), int? dbNo = null)
		功能：Key-Value形式存储对象值，存储结果为json结构
		常用场景：同StringSet
		入参：同StringSet
		出参：同StringSet

	4. T Get<T>(string key, int? dbNo = null)
		功能：获取Key-Value形式存储的对象值
		常用场景：同StringGet
		入参：同StringGet
		出参：缓存的对象值

	Key操作
	1. bool KeyExists(string key, int? dbNo = null)
		功能：判断Key是否存在或过期
		入参：
		key：Redis Key
		dbNo：Redis库号，一般为0~15
		出参：是否存在或过期
	2. bool KeyDelete(string key, int? dbNo = null)
		功能：删除Key
		入参：
		key：Redis Key
		dbNo：Redis库号，一般为0~15
		出参：是否成功

	其他方法的说明请在调用时使用F12查询

C.Redis应用场景参考
redis学习（八）——redis应用场景：https://www.cnblogs.com/xiaoxi/p/7007695.html
Redis的7个应用场景：https://www.cnblogs.com/NiceCui/p/7794659.html

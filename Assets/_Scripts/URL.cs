using System;
using System.IO;
using System.Net;
using System.Text;

public class URL {

	public static string ip;
	public static string username;
	public static string password;

	public static string Request (string url, string extra) {
		
		HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://"+ip+"/"+url);

		string rawData = "username="+username+"&password="+password+"&"+extra;
		byte[] data = Encoding.ASCII.GetBytes(rawData);
		request.Method = "POST";
		request.ContentType = "application/x-www-form-urlencoded";
		request.ContentLength = data.Length;

		request.GetRequestStream().Write(data, 0, data.Length);

		HttpWebResponse response = (HttpWebResponse)request.GetResponse();
		string result = new StreamReader(response.GetResponseStream()).ReadToEnd();
		response.Close();

		return result;
	}

}

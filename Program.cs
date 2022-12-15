using System.Net;
using System.Text;
using System.Text.RegularExpressions;
class Programm
{
    public static List<string> GetLinksFromHTML(string stringHtml)
    {
        var regular = new Regex(@"<a href=""([/\w\d\.\:]+)"".*?>");
        var matches = regular.Matches(stringHtml);
        GroupCollection group;
        var result = new List<string>();
        foreach (Match match in matches)
        {
            group = match.Groups;
            result.Add(group[1].Value);
        }
        return result;
    }

    static string GetResponse(string uri)
    {
        StringBuilder sb = new StringBuilder();
        byte[] buf = new byte[8192];
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        Stream resStream = response.GetResponseStream();
        int count = 0;
        do
        {
            count = resStream.Read(buf, 0, buf.Length);
            if (count != 0)
            {
                sb.Append(Encoding.Default.GetString(buf, 0, count));
            }
        }
        while (count > 0);
        return sb.ToString();
    }
    public static void Main()
    {
        Console.Write("Enter link: ");
        var url = Console.ReadLine();
        string html = GetResponse(url);
        var links = GetLinksFromHTML(html);
        foreach (var link in links)
        {
            Console.WriteLine(link);
        }
    }
}
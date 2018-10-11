using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkdownTest
{
    class Program
    {
        static void Main()
        {
            {
                string input =
                    @"C:\Windows\system32\disksnapshot.exe:  
C:\Windows\system32\disksnapshot1.exe

Base Only:
* C:\Windows\system32\disksnapshot_a.exe
* C:\Windows\system32\disksnapshot_b.exe

Trial Only:
* C:\Windows\system32\disksnapshot_c.exe
* C:\Windows\system32\disksnapshot_d.exe
";

                var result = Markdig.Markdown.ToHtml(input);



                File.WriteAllText("test1.html", result);
            }

            {
                string expected =
                    @"C:\Windows\system32\disksnapshot.exe

Base Only:  
* C:\Windows\system32\disksnapshot_a.exe
* C:\Windows\system32\disksnapshot_b.exe

Trial Only:  
* C:\Windows\system32\disksnapshot_c.exe
* C:\Windows\system32\disksnapshot_d.exe
";

                var result = Markdig.Markdown.ToHtml(expected);

                File.WriteAllText("test2.html", result);
            }

            {
                string input =
                    @"C:\Windows\system32\disksnapshot.exe

Base Only:  
* C:\Windows\system32\disksnapshot_a.exe
* C:\Windows\system32\disksnapshot_b.exe
* C:\Windows\system32\disksnapshot_not_unique.exe
* C:\Windows\system32\disksnapshot_c.exe
* C:\Windows\system32\disksnapshot_d.exe
";

                var result = Markdig.Markdown.ToHtml(input);

                File.WriteAllText("test3.html", result);
            }

            {
                string input =
                    @"C:\Windows\system32\disksnapshot.exe

Trial Only:  
* C:\Windows\system32\disksnapshot1.exe
* C:\Windows\system32\disksnapshot_c.exe
* C:\Windows\system32\disksnapshot_d.exe
";

                var result = Markdig.Markdown.ToHtml(input);

                File.WriteAllText("test4.html", result);
            }

            {
                string input =
                    @"C:\Windows\system32\disksnapshot.exe:  
C:\Windows\system32\disksnapshot1.exe

Trial Only:  
* C:\Windows\system32\disksnapshot_c.exe
* C:\Windows\system32\disksnapshot_d.exe
";

                var result = Markdig.Markdown.ToHtml(input);

                File.WriteAllText("test5.html", result);
            }

            {
                string input =
                    @"C:\Windows\system32\disksnapshot.exe

Base Only:  
* C:\Windows\system32\disksnapshot_a.exe
* C:\Windows\system32\disksnapshot_b.exe

Trial Only:  
* C:\Windows\system32\disksnapshot_c.exe
* C:\Windows\system32\disksnapshot_d.exe
";

                var result = Markdig.Markdown.ToHtml(input);

                File.WriteAllText("test6.html", result);
            }


            {
                string input =
                    @"Base Only:  
C:\Windows\system32\disksnapshot.exe";

                var result = Markdig.Markdown.ToHtml(input);

                File.WriteAllText("test7.html", result);
            }
        }
    }
}

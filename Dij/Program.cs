using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dijkstra
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = 6, en = 9;
            //-1表示不可达
            int[,] a = new int[,] { { 0, 6, 3, -1, -1, -1 }, { 6, 0, 2, 5, -1, -1 }, { 3, 2, 0, 3, 4, -1 }, { -1, 5, 3, 0, 2, 3 }, { -1, -1, 4, 2, 0, 5 }, { -1, -1, -1, 3, 5, 0 } };
            GraphClass gc = new GraphClass();
            gc.CreateMGraph(n, en, a);
            Console.WriteLine(gc.DispMGraph());
            gc.Dijkstra();
            Console.ReadKey();
        }
    }

    public enum zm
    {
        A = 0,
        B = 1,
        C = 2,
        D = 3,
        E = 4,
        F = 5
    }
    struct MGraph
    {
        public int[,] edges;
        public int n, e;
    }
    //S集合
    public class Sset
    {
        public List<int> list;
        public Dictionary<string, int> dic;
        public Sset()
        {
            list = new List<int>();
            dic = new Dictionary<string, int>();
        }
    }
    public class GraphClass
    {
        const int MAXV = 100;
        const int INF = 32767;
        MGraph g = new MGraph();
        private Sset sset = new Sset();//s集合
        private List<int> uList = new List<int>();//u集合
        public GraphClass()
        {
            g.edges = new int[MAXV, MAXV];
        }
        /// <summary>
        /// 创建图的矩阵
        /// </summary>
        /// <param name="n">顶点数</param>
        /// <param name="e">边数</param>
        /// <param name="a">图的矩阵存储</param>
        public void CreateMGraph(int n, int e, int[,] a)
        {
            g.n = n;
            g.e = e;
            for (int i = 0; i < g.n; i++)
            {
                uList.Add(i);
                for (int j = 0; j < g.n; j++)
                {
                    g.edges[i, j] = a[i, j];
                }
            }
        }
        /// <summary>
        /// 图的矩阵输出
        /// </summary>
        /// <returns></returns>
        public string DispMGraph()
        {
            string mystr = string.Empty;
            for (int i = 0; i < g.n; i++)
            {
                if (i == 0)
                {
                    mystr += "   A   B   C   D   E   F\r\n";
                }
                for (int j = 0; j < g.n; j++)
                {
                    if (j == 0)
                    {
                        mystr += (zm)i + "  ";
                    }
                    mystr += string.Format("{0,-4}", g.edges[i, j].ToString());
                }
                mystr += "\r\n";
            }
            return mystr;
        }
        /// <summary>
        /// Dijkstra算法
        /// </summary>
        public void Dijkstra()
        {
            sset.list.Add(0);
            sset.dic.Add("0", 0);
            sset.dic.Add("0-0", 0);
            int max = 100000;
            int uListPostion = 0;
            string path = string.Empty;
            while (uList.Count > 1)
            {
                foreach (KeyValuePair<string, int> item in sset.dic)
                {
                    string[] temp = item.Key.Split('-');
                    int last = Convert.ToInt32(temp[temp.Length - 1]);
                    for (int i = 0; i < uList.Count; i++)
                    {
                        if (!temp.Contains(uList[i].ToString()))
                        {
                            if (g.edges[last, uList[i]] != -1 && g.edges[last, uList[i]] != 0 && (g.edges[last, uList[i]] + item.Value) < max)
                            {
                                max = g.edges[last, uList[i]];
                                uListPostion = i;
                                path = item.Key;
                            }
                        }
                    }
                }
                sset.list.Add(uList[uListPostion]);
                sset.dic.Add(path + "-" + uList[uListPostion], max + sset.dic[path]);
                uList.Remove(uList[uListPostion]);
                max = 100000;
            }
            //输出路线和相应的距离
            foreach (KeyValuePair<string, int> item in sset.dic)
            {
                string[] temp = item.Key.Split('-');
                string consolestring = "";
                foreach (string itemstr in temp)
                {
                    consolestring += (zm)(Convert.ToInt32(itemstr)) + "-";
                }
                consolestring = consolestring.Remove(consolestring.LastIndexOf('-'));
                Console.WriteLine(consolestring + "  距离：" + item.Value);
            }
        }
    }
}

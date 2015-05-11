using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
//using System.Management.ManagementObjectSearcher;
using System.Management;


namespace Dz_Tree_Proc1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Process[] localAll = Process.GetProcesses();
           // myTreeViewItem root = new myTreeViewItem { Title = "Root" };
           //
           // myTreeViewItem childItem1 = new myTreeViewItem { Title = "Child Item 1" };
           //
           // childItem1.Items.Add(new myTreeViewItem { Title = "Child Item 1.1" });
           // childItem1.Items.Add(new myTreeViewItem { Title = "Child Item 1.2" });
           // childItem1.Items.Add(new myTreeViewItem { Title = "Child Item 1.3" });
           //
           // root.Items.Add(childItem1);
           //
           // root.Items.Add(new myTreeViewItem { Title = "Child Item 2" });
            //myTreeViewItem MAINTree = new myTreeViewItem(); 
            List<string> Namee = new List<string>();
            foreach (var el in localAll)
            {

                ProcessTree Tree = new ProcessTree(el);
                tvDemo.Items.Add(Tree);
                Namee.Add(Tree.ProcessName); 
               // foreach (var el1 in tvDemo.Items )
               // {
               //     var k = (el1 as ProcessTree).ProcessName;
               //     //MessageBox.Show(k);
               // }
                
            }

            foreach (var el in Namee)
            {
                MessageBox.Show(el);
            }
           
            //tvDemo.Items.Add(MAINTree);
        }
    }
    public class myTreeViewItem
    {
       // public List<myTreeViewItem> Items { set; get; }
        public Dictionary<string,ProcessTree> ChildProcesses { set; get; }

        public string Title { set; get; }
        public myTreeViewItem()
        {
            ChildProcesses = new Dictionary<string, ProcessTree>();
           // Items = new List<myTreeViewItem>();
        }
    }
    public static class ProcessExtensions  
{
    /// <summary>
    /// Get the child processes for a given process
    /// </summary>
    /// <param name="process"></param>
    /// <returns></returns>
    public static List<Process> GetChildProcesses(this Process process)
    {
        var results = new List<Process>();

        // query the management system objects for any process that has the current
        // process listed as it's parentprocessid
        string queryText = string.Format("select processid from win32_process where parentprocessid = {0}", process.Id);
        using (var searcher = new ManagementObjectSearcher(queryText))
        {
            foreach (var obj in searcher.Get())
            {
                object data = obj.Properties["processid"].Value;
                if (data != null)
                {
                    // retrieve the process
                    var childId = Convert.ToInt32(data);
                    var childProcess = Process.GetProcessById(childId);

                    // ensure the current process is still live
                    if (childProcess != null)
                        results.Add(childProcess);
                }
            }
        }
        return results;
    }
    /// <summary>
    /// Get the Parent Process ID for a given process
    /// </summary>
    /// <param name="process"></param>
    /// <returns></returns>
    public static int? GetParentId(this Process process)
    {
        // query the management system objects
        string queryText = string.Format("select parentprocessid from win32_process where processid = {0}", process.Id);
        using (var searcher = new ManagementObjectSearcher(queryText))
        {
            foreach (var obj in searcher.Get())
            {
                object data = obj.Properties["parentprocessid"].Value;
                if (data != null)
                    return Convert.ToInt32(data);
            }
        }
        return null;
    }
}
   public class ProcessTree  
{
  
    public ProcessTree(Process process)
    {
        this.Process = process;
        InitChildren();
    }

    // Recurively load children
    void InitChildren()
    {
        this.ChildProcesses = new List<ProcessTree>();

        // retrieve the child processes
        var childProcesses = this.Process.GetChildProcesses();

        // recursively build children
        foreach (var childProcess in childProcesses)
            this.ChildProcesses.Add(new ProcessTree(childProcess));
    }

    public Process Process { get; set; }

    public List<ProcessTree> ChildProcesses { get; set; }

    public int Id { get { return Process.Id; } }

    public string ProcessName { get { return Process.ProcessName; } }

    public long Memory { get { return Process.PrivateMemorySize64; } }

}
}

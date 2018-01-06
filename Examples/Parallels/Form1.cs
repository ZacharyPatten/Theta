using System;
using System.Windows.Forms;
using Theta.Parallels;
using System.Threading;

namespace Parallels
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
			InitializeEvents();
		}
		
		public void InitializeEvents()
		{
			ux_singleRun_button.Click +=
				(object sender, EventArgs e) =>
					{
						ux_single_progressBar.Value = 0;
						ux_singleRun_button.Enabled = false;
						ux_singleCurrentlyRunning_label.Text = "Yes";
						int progress = 0;
						Parallel.Thread(
							(Parallel.Callback report) =>
							{
								for (int i = 1; (i <= 10); i++)
								{
									System.Threading.Thread.Sleep(500);
									progress = i * 10;
									report();
								}
							},
							() =>
							{
								ux_single_progressBar.Value = progress;
							},
							(IAsyncResult ar) =>
							{
								ux_singleCurrentlyRunning_label.Text = "No";
								ux_singleRun_button.Enabled = true;
							});
					};

			ux_multipleRun_button.Click +=
				(object sender, EventArgs e) =>
				{
					// disable/manage controls
					ux_multiple_numericUpDown.Enabled = false;
					ux_multipleRun_button.Enabled = false;
					ux_multipleCurrentlyRunning_label.Text = "Yes";
					ux_multiple_listBox.Items.Clear();

					int threads = (int)ux_multiple_numericUpDown.Value;
					int[] progress = new int[threads];
					bool[] completed = new bool[threads];
					DateTime[] startTimes = new DateTime[threads];
					Random random = new Random();

					for (int i = 1; i <= threads; i++)
					{
						int current = i;
						ux_multiple_listBox.Items.Add("Thread " + current + ": 0%");
						startTimes[current - 1] = DateTime.Now;
						Parallel.Thread(
							(Parallel.Callback report) =>
							{
								// the work done by each background thread
								for (int j = 1; (j <= 10); j++)
								{
									System.Threading.Thread.Sleep(random.Next(100, 1500));
									progress[current - 1] = j * 10;
									report();
								}
							},
							() =>
							{
								// update the progress
								ux_multiple_listBox.Items[current - 1] = "Thread " + current + ": " + progress[current - 1] + "%";
							},
							(IAsyncResult ar) =>
							{
								ux_multiple_listBox.Items[current - 1] = "Thread " + current + ": 100% (Time: " + (DateTime.Now - startTimes[current - 1]) + ")";

								/// return if any of the threads are not complete
								completed[current - 1] = true;
								foreach (bool boolean in completed)
									if (boolean == false)
										return;

								// if all the threads are completed
								ux_multipleCurrentlyRunning_label.Text = "No";
								ux_multipleRun_button.Enabled = true;
								ux_multiple_numericUpDown.Enabled = true;
							});
					}
				};
		}
	}
}

using System;


namespace InfraAsApi_Rest.Models
{
    public class ProcessPerformanceCounter : IComparable<ProcessPerformanceCounter>
    {
        private double cpuUsage;
        private double memoryUsage;
        private string processName;

        public ProcessPerformanceCounter(string processName)
        {
            this.ProcessName = processName;
            this.InitPerformanceCounter(processName);
        }

        public double MemoryUsage { get => memoryUsage; set => memoryUsage = value; }
        public double CpuUsage { get => cpuUsage; set => cpuUsage = value; }
        public string ProcessName { get => processName; set => processName = value; }

        public int CompareTo(ProcessPerformanceCounter item)
        {
            var actualCpuValue = this.CpuUsage;
            var otherCpuValue = item.CpuUsage;
            if (actualCpuValue == otherCpuValue)
            {
                return 0;
            }
            return actualCpuValue > otherCpuValue ? -1 : 1;
        }

        private void InitPerformanceCounter(string processName)
        {
            try
            {

                var cpuTime = new System.Diagnostics.PerformanceCounter("Process", "% Processor Time", processName);
                var memory = new System.Diagnostics.PerformanceCounter("Process", "Working Set", processName);
                this.CpuUsage = cpuTime.NextValue();
                this.MemoryUsage = memory.NextValue();

                this.CpuUsage = Math.Round(cpuTime.NextValue() / Environment.ProcessorCount, 2);
                this.MemoryUsage = Math.Round(memory.NextValue() / 1024 / 1024, 2);
            }
            catch
            {

            }

        }




    }
}

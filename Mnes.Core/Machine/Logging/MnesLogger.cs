﻿using System.Diagnostics;

namespace Mnes.Core.Machine.Logging;

public sealed class MnesLogger {
   public List<InstructionLog> CpuLog { get; init; } = new();

   public IEnumerable<InstructionLog> GetLast(int count) {
      if (count > CpuLog.Count) count = CpuLog.Count;
      return CpuLog.GetRange(CpuLog.Count - count, count);
   }

   public void Log(InstructionLog log) {
      CpuLog.Add(log);
      Debug.WriteLine(CpuLog.Count.ToString().PadLeft(4) + " " + log);
   }
}

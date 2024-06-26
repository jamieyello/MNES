﻿using Mnes.Core.Utility;

namespace Mnes.Core.Machine.PPU.Registers;

public sealed class PpuCtrl : Register {
   public enum SpriteSizeType {
      _8x8,
      _8x16,
   }

   public ushort BaseNameTableAddress { get; private set; }
   public byte VramAddressIncrement { get; private set; }
   public ushort SpritePatternTableAddress8x8 { get; private set; }
   public ushort BackgroundTableAddress { get; private set; }
   public SpriteSizeType SpriteSize { get; private set; }
   /// <summary> 0: read backdrop from EXT pins; 1: output color on EXT pins </summary>
   public bool MasterSlaveSelect { get; private set; }
   public bool NMIEnabled { get; private set; }

   public PpuCtrl(MachineState m) : base(m) { }

   public override void CpuWrite(byte value) {
      BaseNameTableAddress = (Value & 0b_0000_0011) switch {
         0 => 0x2000,
         1 => 0x2400,
         2 => 0x2800,
         3 => 0x2C00,
         _ => throw new Exception($"impossible value ({Value})"),
      };

      VramAddressIncrement = (byte)(value.HasBit(2) ? 1 : 32);
      SpritePatternTableAddress8x8 = (ushort)(value.HasBit(3) ? 0x0000 : 0x1000);
      BackgroundTableAddress = (ushort)(!value.HasBit(4) ? 0x0000 : 0x1000); // double check this
      SpriteSize = !value.HasBit(5) ? SpriteSizeType._8x8 : SpriteSizeType._8x16;
      MasterSlaveSelect = (value & BitFlags.F6) > 0;
      NMIEnabled = value.HasBit(7);
      Machine.Ppu.Registers.PpuScroll.XHighBit = value.HasBit(0);
      Machine.Ppu.Registers.PpuScroll.YHighBit = value.HasBit(1);
      Machine.Ppu.Registers.OpenBus = value;
   }

   public override byte CpuRead() =>
      Machine.Ppu.Registers.OpenBus;

   public override void SetPowerUpState() => CpuWrite(0);
}

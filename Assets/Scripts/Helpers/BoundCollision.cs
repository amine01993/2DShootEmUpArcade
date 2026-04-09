

using System;

public class BoundCollision
{
	private Bounds bound = Bounds.None;

	public void Add(Bounds newBound)
	{
		bound |= newBound;
	}

	public void Remove(Bounds oldBound)
	{
		bound &= ~oldBound;
	}

	public bool Has(Bounds bound)
	{
		return (this.bound & bound) != 0;
	}

	[Flags]
	public enum Bounds
	{
		None = 0b0000, Top = 0b0001, Bottom = 0b0010, Left = 0b0100, Right = 0b1000
	}
}


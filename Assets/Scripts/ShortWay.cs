using System.Collections.Generic;
using System;
using Unity.VisualScripting;

public class Cell
{
	private int x;
	private int y;
	public int getx()
	{
		return x;
	}
	public int gety()
	{
		return y;
	}
	public void setx(int x)
	{
		this.x = x;
	}
	public void sety(int y)
	{
		this.y = y;
	}
	public static bool operator == (Cell ImpliedObject, Cell o)
	{
		return ImpliedObject.x == o.x && ImpliedObject.y == o.y;
	}
	public static bool operator != (Cell ImpliedObject, Cell o)
	{
		return ImpliedObject.x != o.x && ImpliedObject.y != o.y;
	}
//C++ TO C# CONVERTER NOTE: This 'CopyFrom' method was converted from the original copy assignment operator:
//ORIGINAL LINE: Cell operator =(Cell o)
	public Cell CopyFrom(Cell o)
	{
		x = o.x;
		y = o.y;
		return this;
	}
	public Cell(int x, int y)
	{
		this.x = x;
		this.y = y;
	}
	public Cell()
	{
		this.x = 0;
		this.y = 0;
	}
}



public class Globals
{
	public static List<Cell> getShortestPath(Cell ori, Cell dest, int[] array, int width, int height)
	{
		if (ori == dest)
		{
			return new List<Cell>();
		}
		uint[] sizes = new uint[width * height];
		Cell[] prev = Arrays.InitializeWithDefaultInstances<Cell>(width * height);
		
		for (int i = 0;i < width * height;i++)
		{
			sizes[i] = 4294967295;
			prev[i] = new Cell(-1, -1);
		}
		
		sizes[ori.getx() + ori.gety() * width] = 0;
		prev[ori.getx() + ori.gety() * width].CopyFrom(ori);
		Queue<Cell> porVisitar = new Queue<Cell>();
		porVisitar.Enqueue(ori);
		
		while (porVisitar.Count > 0)
		{
			Cell cur = porVisitar.Peek();
			porVisitar.Dequeue();
			/*Console.Write(porVisitar.Count);
			Console.Write("\n");*/
			
			for (int i = -1;i < 2;i++)
			{
				for (int j = -1;j < 2;j++)
				{
					if ((cur.getx() + j) >= 0 && (cur.getx() + j) < width && (cur.gety() + i) >= 0 && (cur.gety() + i) < height && array[(cur.getx() + j) + (cur.gety() + i) * width] == 0 && sizes[cur.getx() + cur.gety() * width] + 1 < sizes[(cur.getx() + j) + (cur.gety() + i) * width])
					{
						sizes[(cur.getx() + j) + (cur.gety() + i) * width] = sizes[cur.getx() + cur.gety() * width] + 1;
						prev[(cur.getx() + j) + (cur.gety() + i) * width] = new Cell(cur.getx(), cur.gety());
						porVisitar.Enqueue(new Cell(cur.getx() + j, cur.gety() + i));
					}
				}
			}
		}
		if (prev[dest.getx() + dest.gety() * width] == new Cell(-1, -1))
		{
			return new List<Cell>();
		}

		Cell pp = new Cell(dest.getx(), dest.gety());
		List<Cell> res = new List<Cell>(Arrays.InitializeWithDefaultInstances<Cell>(Convert.ToInt32(sizes[(dest.getx() + dest.gety() * width)] + 1)));
		
		for (int i = res.Count - 1; !(pp == ori); i--)
		{
			res[i].CopyFrom(pp);
			pp.CopyFrom(prev[pp.getx() + pp.gety() * width]);
		}
		return new List<Cell>(res);
	}
}


//Helper class added by C++ to C# Converter:
//----------------------------------------------------------------------------------------
//	Copyright © 2006 - 2022 Tangible Software Solutions, Inc.
//	This class can be used by anyone provided that the copyright notice remains intact.
//
//	This class provides the ability to initialize and delete array elements.
//----------------------------------------------------------------------------------------
internal static class Arrays
{
	public static T[] InitializeWithDefaultInstances<T>(int length) where T : class, new()
	{
		T[] array = new T[length];
		for (int i = 0; i < length; i++)
		{
			array[i] = new T();
		}
		return array;
	}
	public static string[] InitializeStringArrayWithDefaultInstances(int length)
	{
		string[] array = new string[length];
		for (int i = 0; i < length; i++)
		{
			array[i] = "";
		}
		return array;
	}
	public static T[] PadWithNull<T>(int length, T[] existingItems) where T : class
	{
		if (length > existingItems.Length)
		{
			T[] array = new T[length];
			for (int i = 0; i < existingItems.Length; i++)
			{
				array[i] = existingItems[i];
			}
			return array;
		}
		else
			return existingItems;
	}
	public static T[] PadValueTypeArrayWithDefaultInstances<T>(int length, T[] existingItems) where T : struct
	{
		if (length > existingItems.Length)
		{
			T[] array = new T[length];
			for (int i = 0; i < existingItems.Length; i++)
			{
				array[i] = existingItems[i];
			}
			return array;
		}
		else
			return existingItems;
	}
	public static T[] PadReferenceTypeArrayWithDefaultInstances<T>(int length, T[] existingItems) where T : class, new()
	{
		if (length > existingItems.Length)
		{
			T[] array = new T[length];
			for (int i = 0; i < existingItems.Length; i++)
			{
				array[i] = existingItems[i];
			}
			for (int i = existingItems.Length; i < length; i++)
			{
				array[i] = new T();
			}
			return array;
		}
		else
			return existingItems;
	}
	public static string[] PadStringArrayWithDefaultInstances(int length, string[] existingItems)
	{
		if (length > existingItems.Length)
		{
			string[] array = new string[length];
			for (int i = 0; i < existingItems.Length; i++)
			{
				array[i] = existingItems[i];
			}
			for (int i = existingItems.Length; i < length; i++)
			{
				array[i] = "";
			}
			return array;
		}
		else
			return existingItems;
	}
	public static void DeleteArray<T>(T[] array) where T: System.IDisposable
	{
		foreach (T element in array)
		{
			if (element != null)
				element.Dispose();
		}
	}
}
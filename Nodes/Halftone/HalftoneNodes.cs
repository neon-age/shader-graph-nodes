﻿#if UNITY_EDITOR
using UnityEditor.ShaderGraph;
using System.Reflection;
using UnityEngine;

[Title("Halftone", "Halftone Circle")]
public class HalftoneCircleNode : CodeFunctionNode {
	public HalftoneCircleNode() {
		name = "Halftone Circle";
	}

	protected override MethodInfo GetFunctionToConvert() {
		return GetType().GetMethod("HalftoneCircle", BindingFlags.Static | BindingFlags.NonPublic);
	}

	static string HalftoneCircle([Slot(0, Binding.None)] Vector1 Base, [Slot(1, Binding.None)] Vector2 UV, [Slot(2, Binding.None, 0.01f, 0, 0, 0)] Vector2 Offset, [Slot(3, Binding.None)] out Vector1 Out) {
		return @"
{
	float Scale = 0.78;
	float2 Direction = Offset/dot(Offset, Offset);
	float2 Normal = {-Direction.y, Direction.x};
	float2 Position = {fmod(abs(dot(UV, Direction)) + 0.5, 1) - 0.5, fmod(abs(dot(UV, Normal)) + 0.5, 1) - 0.5};
	Out = Scale*dot(Position, Position)/(0.25*(1-Base));
	Out = 1-saturate((1 - Out) / fwidth(Out));
}
";
	}
}

[Title("Halftone", "Halftone Circle Color")]
public class HalftoneCircleColorNode : CodeFunctionNode {
	public HalftoneCircleColorNode() {
		name = "Halftone Circle Color";
	}

	protected override MethodInfo GetFunctionToConvert() {
		return GetType().GetMethod("HalftoneCircleColor", BindingFlags.Static | BindingFlags.NonPublic);
	}

	static string HalftoneCircleColor([Slot(0, Binding.None)] Vector3 Base, [Slot(1, Binding.None)] Vector2 UV, [Slot(2, Binding.None, 0.01f, 0, 0, 0)] Vector2 OffsetR, [Slot(3, Binding.None, 0.00866f, 0.005f, 0, 0)] Vector2 OffsetG, [Slot(4, Binding.None, 0.005f, 0.00866f, 0, 0)] Vector2 OffsetB, [Slot(5, Binding.None)] out Vector3 Out) {
		Out = new Vector3();
		return @"
{
	float Scale = 0.78;
	float2 Direction = OffsetR/dot(OffsetR, OffsetR);
	float2 Normal = {-Direction.y, Direction.x};
	float2 Position = {fmod(abs(dot(UV, Direction)) + 0.5, 1) - 0.5, fmod(abs(dot(UV, Normal)) + 0.5, 1) - 0.5};
	Out.x = Scale*dot(Position, Position)/(0.25*(1-Base.x));
	float2 Direction2 = OffsetG/dot(OffsetG, OffsetG);
	float2 Normal2 = {-Direction2.y, Direction2.x};
	float2 Position2 = {fmod(abs(dot(UV, Direction2)) + 0.5, 1) - 0.5, fmod(abs(dot(UV, Normal2)) + 0.5, 1) - 0.5};
	Out.y = Scale*dot(Position2, Position2)/(0.25*(1-Base.y));
	float2 Direction3 = OffsetB/dot(OffsetB, OffsetB);
	float2 Normal3 = {-Direction3.y, Direction3.x};
	float2 Position3 = {fmod(abs(dot(UV, Direction3)) + 0.5, 1) - 0.5, fmod(abs(dot(UV, Normal3)) + 0.5, 1) - 0.5};
	Out.z = Scale*dot(Position3, Position3)/(0.25*(1-Base.z));
	Out = 1-saturate((1 - Out) / fwidth(Out));
}
";
	}
}

[Title("Halftone", "Halftone Smooth")]
public class HalftoneSmoothNode : CodeFunctionNode {
	public HalftoneSmoothNode() {
		name = "Halftone Smooth";
	}

	protected override MethodInfo GetFunctionToConvert() {
		return GetType().GetMethod("HalftoneSmooth", BindingFlags.Static | BindingFlags.NonPublic);
	}

	static string HalftoneSmooth([Slot(0, Binding.None)] Vector1 Base, [Slot(1, Binding.None)] Vector2 UV, [Slot(2, Binding.None, 0.01f, 0, 0, 0)] Vector2 Offset, [Slot(3, Binding.None)] out Vector1 Out) {
		return @"
{
	float2 Direction = Offset/dot(Offset, Offset);
	float2 Normal = {-Direction.y, Direction.x};
	float2 Position = {fmod(abs(dot(UV, Direction)) + 0.5, 1) - 0.5, fmod(abs(dot(UV, Normal)) + 0.5, 1) - 0.5};
	float2 Position2 = {fmod(Position.x + 1, 1) - 0.5, fmod(Position.y + 1, 1) - 0.5};
	float dp1 = dot(Position, Position);
	float dp2 = dot(Position2, Position2);
	float t = dp1/(dp1 + dp2);
	Out =  (1-t)*(dp1 - 0.25*(1-Base))-t*(dp2 - 0.25*Base);
	Out = 1-saturate((-Out) / fwidth(Out));
}
";
	}
}

[Title("Halftone", "Halftone Smooth Color")]
public class HalftoneSmoothColorNode : CodeFunctionNode {
	public HalftoneSmoothColorNode() {
		name = "Halftone Smooth Color";
	}

	protected override MethodInfo GetFunctionToConvert() {
		return GetType().GetMethod("HalftoneSmoothColor", BindingFlags.Static | BindingFlags.NonPublic);
	}

	static string HalftoneSmoothColor([Slot(0, Binding.None)] Vector3 Base, [Slot(1, Binding.None)] Vector2 UV, [Slot(2, Binding.None, 0.01f, 0, 0, 0)] Vector2 OffsetR, [Slot(3, Binding.None, 0.00866f, 0.005f, 0, 0)] Vector2 OffsetG, [Slot(4, Binding.None, 0.005f, 0.00866f, 0, 0)] Vector2 OffsetB, [Slot(5, Binding.None)] out Vector3 Out) {
		Out = new Vector3();
		return @"
{
	float2 Direction = OffsetR/dot(OffsetR, OffsetR);
	float2 Normal = {-Direction.y, Direction.x};
	float2 Position1 = {fmod(abs(dot(UV, Direction)) + 0.5, 1) - 0.5, fmod(abs(dot(UV, Normal)) + 0.5, 1) - 0.5};
	float2 Position2 = {fmod(Position1.x + 1, 1) - 0.5, fmod(Position1.y + 1, 1) - 0.5};
	float dp1 = dot(Position1, Position1);
	float dp2 = dot(Position2, Position2);
	float t = dp1/(dp1 + dp2);
	Out.x =  (1-t)*(dp1 - 0.25*(1-Base.x))-t*(dp2 - 0.25*Base.x);
	float2 Direction2 = OffsetG/dot(OffsetG, OffsetG);
	float2 Normal2 = {-Direction2.y, Direction2.x};
	float2 Position12 = {fmod(abs(dot(UV, Direction2)) + 0.5, 1) - 0.5, fmod(abs(dot(UV, Normal2)) + 0.5, 1) - 0.5};
	float2 Position22 = {fmod(Position12.x + 1, 1) - 0.5, fmod(Position12.y + 1, 1) - 0.5};
	float dp12 = dot(Position12, Position12);
	float dp22 = dot(Position22, Position22);
	float t2 = dp12/(dp12 + dp22);
	Out.y =  (1-t2)*(dp12 - 0.25*(1-Base.y))-t2*(dp22 - 0.25*Base.y);
	float2 Direction3 = OffsetB/dot(OffsetB, OffsetB);
	float2 Normal3 = {-Direction3.y, Direction3.x};
	float2 Position13 = {fmod(abs(dot(UV, Direction3)) + 0.5, 1) - 0.5, fmod(abs(dot(UV, Normal3)) + 0.5, 1) - 0.5};
	float2 Position23 = {fmod(Position13.x + 1, 1) - 0.5, fmod(Position13.y + 1, 1) - 0.5};
	float dp13 = dot(Position13, Position13);
	float dp23 = dot(Position23, Position23);
	float t3 = dp13/(dp13 + dp23);
	Out.z =  (1-t3)*(dp13 - 0.25*(1-Base.z))-t3*(dp23 - 0.25*Base.z);
	Out = 1-saturate((- Out) / fwidth(Out));
}
";
	}
}

[Title("Halftone", "Halftone Square")]
public class HalftoneSquareNode : CodeFunctionNode {
	public HalftoneSquareNode() {
		name = "Halftone Square";
	}

	protected override MethodInfo GetFunctionToConvert() {
		return GetType().GetMethod("HalftoneSquare", BindingFlags.Static | BindingFlags.NonPublic);
	}

	static string HalftoneSquare([Slot(0, Binding.None)] Vector1 Base, [Slot(1, Binding.None)] Vector2 UV, [Slot(2, Binding.None, 0.01f, 0, 0, 0)] Vector2 Offset, [Slot(3, Binding.None)] out Vector1 Out) {
		return @"
{
	float2 Direction = Offset/dot(Offset, Offset);
	float2 Normal = {-Direction.y, Direction.x};
	float2 Position = {fmod(abs(dot(UV, Direction)) + 0.5, 1) - 0.5, fmod(abs(dot(UV, Normal)) + 0.5, 1) - 0.5};
	float Radius = 0.5*sqrt(1-Base);
	Out = max(abs(Position.x), abs(Position.y))/Radius;
	Out = 1-saturate((1 - Out) / fwidth(Out));
}
";
	}
}

[Title("Halftone", "Halftone Square Color")]
public class HalftoneSquareColorNode : CodeFunctionNode {
	public HalftoneSquareColorNode() {
		name = "Halftone Square Color";
	}

	protected override MethodInfo GetFunctionToConvert() {
		return GetType().GetMethod("HalftoneSquareColor", BindingFlags.Static | BindingFlags.NonPublic);
	}

	static string HalftoneSquareColor([Slot(0, Binding.None)] Vector3 Base, [Slot(1, Binding.None)] Vector2 UV, [Slot(2, Binding.None, 0.01f, 0, 0, 0)] Vector2 OffsetR, [Slot(3, Binding.None, 0.00866f, 0.005f, 0, 0)] Vector2 OffsetG, [Slot(4, Binding.None, 0.005f, 0.00866f, 0, 0)] Vector2 OffsetB, [Slot(5, Binding.None)] out Vector3 Out) {
		Out = new Vector3();
		return @"
{
	float2 Direction = OffsetR/dot(OffsetR, OffsetR);
	float2 Normal = {-Direction.y, Direction.x};
	float2 Position = {fmod(abs(dot(UV, Direction)) + 0.5, 1) - 0.5, fmod(abs(dot(UV, Normal)) + 0.5, 1) - 0.5};
	float Radius = 0.5*sqrt(1-Base.x);
	Out.x = max(abs(Position.x), abs(Position.y))/Radius;
	float2 Direction2 = OffsetG/dot(OffsetG, OffsetG);
	float2 Normal2 = {-Direction2.y, Direction2.x};
	float2 Position2 = {fmod(abs(dot(UV, Direction2)) + 0.5, 1) - 0.5, fmod(abs(dot(UV, Normal2)) + 0.5, 1) - 0.5};
	float Radius2 = 0.5*sqrt(1-Base.y);
	Out.y = max(abs(Position2.x), abs(Position2.y))/Radius2;
	float2 Direction3 = OffsetB/dot(OffsetB, OffsetB);
	float2 Normal3 = {-Direction3.y, Direction3.x};
	float2 Position3 = {fmod(abs(dot(UV, Direction3)) + 0.5, 1) - 0.5, fmod(abs(dot(UV, Normal3)) + 0.5, 1) - 0.5};
	float Radius3 = 0.5*sqrt(1-Base.z);
	Out.z = max(abs(Position3.x), abs(Position3.y))/Radius3;
	Out = 1-saturate((1 - Out) / fwidth(Out));
}
";
	}
}
#endif

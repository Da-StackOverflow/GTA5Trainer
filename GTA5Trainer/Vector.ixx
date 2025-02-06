export module Vector;

import "Base.h";
import <string>;

export struct Vector2
{
public:
	float x;
	float y;

	constexpr Vector2(float x, float y) noexcept : x(x), y(y)
	{
	}
	constexpr Vector2(Vector2* p) noexcept : x(p->x), y(p->y)
	{
	}
	constexpr Vector2(const Vector2& p) noexcept : x(p.x), y(p.y)
	{
	}

	Temp1(T)
	constexpr Vector2(T* addr) noexcept
	{
		var p = reinterpret_cast<Vector2*>(addr);
		x = p->x;
		y = p->y;
	}

	constexpr Vector2() noexcept : x(0.0f), y(0.0f)
	{
	}

	constexpr bool operator==(const Vector2& p) const noexcept
	{
		return x == p.x && y == p.y;
	}
	constexpr bool operator!=(const Vector2& p) const noexcept
	{
		return x!= p.x || y!= p.y;
	}
	constexpr Vector2 operator+(const Vector2& p) const noexcept
	{
		return Vector2(x + p.x, y + p.y);
	}
	constexpr Vector2 operator-(const Vector2& p) const noexcept
	{
		return Vector2(x - p.x, y - p.y);
	}
};

export struct Vector3
{
	f64 x;
	f64 y;
	f64 z;

	constexpr Vector3(float x, float y, float z) noexcept : x(x), y(y), z(z)
	{
	}
	constexpr Vector3(Vector3* p) noexcept : x(p->x), y(p->y), z(p->z)
	{
	}
	constexpr Vector3(const Vector3& p) noexcept : x(p.x), y(p.y), z(p.z)
	{
	}

	Temp1(T)
	constexpr Vector3(T* addr) noexcept
	{
		var p = reinterpret_cast<Vector3*>(addr);
		x = p->x;
		y = p->y;
		z = p->z;
	}

	constexpr Vector3() noexcept : x(0.0), y(0.0), z(0.0)
	{
	}

	constexpr bool operator==(const Vector3& p) const noexcept
	{
		return x == p.x && y == p.y && z == p.z;
	}
	constexpr bool operator!=(const Vector3& p) const noexcept
	{
		return x!= p.x || y!= p.y || z!= p.z;
	}
	constexpr Vector3 operator+(const Vector3& p) const noexcept
	{
		return Vector3(x + p.x, y + p.y, z + p.z);
	}
	constexpr Vector3 operator-(const Vector3& p) const noexcept
	{
		return Vector3(x - p.x, y - p.y, z - p.z);
	}

	String ToString() const
	{
		char buffer[100]{0};
		sprintf_s(buffer, "(%.3f, %.3f, %.3f)", x, y, z);
		return buffer;
	}
};

static_assert(sizeof(Vector3) == 24, "");
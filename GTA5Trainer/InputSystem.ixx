export module InputSystem;

import "Base.h";
import KeyCode;
import Util;

struct KeyStates
{
public:
	int key;
	bool isDown;

	constexpr KeyStates() : key(0), isDown(false)
	{

	}

	constexpr KeyStates(int key) : key(key), isDown(false)
	{

	}

	bool operator==(const KeyStates& other) const
	{
		return key == other.key;
	}

	bool operator!=(const KeyStates& other) const
	{
		return key != other.key;
	}
};

class InputSystem
{
private:
	static const uint KeyCodeType = 255;
	static const uint MaxPeriodTime = 100;

	KeyStates _keyStates[KeyCodeType];
public:
	InputSystem()
	{
		for (uint i = 0; i < KeyCodeType; i++)
		{
			_keyStates[i].key = i;
		}
	}

	void OnKeyDown(uint key)
	{
		_keyStates[key].isDown = true;
	}

	void OnKeyUp(uint key)
	{
		_keyStates[key].isDown = false;
	}

	bool IsKeyDown(KeyCode keyCode) const
	{
		return _keyStates[keyCode].isDown;
	}

	bool IsAccept() const
	{
		return IsKeyDown(KeyCode::Num5) || IsKeyDown(KeyCode::Return);
	}

	bool IsBack() const
	{
		return IsKeyDown(KeyCode::Num0) || IsKeyDown(KeyCode::Back);
	}

	bool IsUp() const
	{
		return IsKeyDown(KeyCode::Num8) || IsKeyDown(KeyCode::Up);
	}

	bool IsDown() const
	{
		return IsKeyDown(KeyCode::Num2) || IsKeyDown(KeyCode::Down);
	}

	bool IsLeft() const
	{
		return IsKeyDown(KeyCode::Num4) || IsKeyDown(KeyCode::Left);
	}

	bool IsRight() const
	{
		return IsKeyDown(KeyCode::Num6) || IsKeyDown(KeyCode::Right);
	}

	bool IsShift() const
	{
		return IsKeyDown(KeyCode::Shift);
	}

	bool IsSpace() const
	{
		return IsKeyDown(KeyCode::Space);
	}

	bool MenuSwitchPressed() const
	{
		return IsKeyDown(KeyCode::F4);
	}
};

export InputSystem Input = InputSystem();
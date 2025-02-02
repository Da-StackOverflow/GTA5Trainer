internal abstract class MenuItem
{
	public MenuItem(string text, int width, int height, Color bgColor, Color textColor)
	{
		Width = width / 1000.0f;
		Height = height / 1000.0f;
		_bgColor = bgColor;
		_textColor = textColor;
		Text = text;
		_scale = new() { X = 0, Y = Height * 8.0f };
	}

	public float Width { get; set; }
	public float Height { get; set; }
	protected static int PlayerPed => Function.PLAYER_PED_ID();
	protected static int PlayerID => Function.PLAYER_ID();

	public Color _bgColor;
	public ref Color BgColor => ref _bgColor;

	public Color _textColor;
	public ref Color TextColor => ref _textColor;

	public virtual string Text { get; set; }

	protected Vector2 _position;
	public ref Vector2 Position => ref _position;

	protected Vector2 _textPosition;
	public ref Vector2 TextPosition => ref _textPosition;

	protected Vector2 _scale;
	public ref Vector2 Scale => ref _scale;
}

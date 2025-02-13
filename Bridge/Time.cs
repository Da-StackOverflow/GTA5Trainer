namespace Bridge
{
	public static class Time
	{
		public static long Now => System.DateTime.Now.Ticks / 10000;
	}
}

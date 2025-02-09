
[Serializable]
public class Car
{
	public string? Name { get; set; }
	public DisplayName? DisplayName { get; set; }
	public long Hash { get; set; }
	public long SignedHash { get; set; }
	public string? HexHash { get; set; }
	public string? DlcName { get; set; }
	public string? HandlingId { get; set; }
	public string? LayoutId { get; set; }
	public string? Manufacturer { get; set; }
	public ManufacturerDisplayName? ManufacturerDisplayName { get; set; }
	public string? Class { get; set; }
	public int ClassId { get; set; }
	public string? Type { get; set; }
	public string? PlateType { get; set; }
	public string? DashboardType { get; set; }
	public string? WheelType { get; set; }
	public List<string?>? Flags { get; set; }
	public int Seats { get; set; }
	public int Price { get; set; }
	public int MonetaryValue { get; set; }
	public bool HasConvertibleRoof { get; set; }
	public bool HasSirens { get; set; }
	public List<string?>? Weapons { get; set; }
	public List<string?>? ModKits { get; set; }
	public Dimensions? DimensionsMin { get; set; }
	public Dimensions? DimensionsMax { get; set; }
	public Dimensions? BoundingCenter { get; set; }
	public double BoundingSphereRadius { get; set; }
	public object? Rewards { get; set; }
	public double MaxBraking { get; set; }
	public double MaxBrakingMods { get; set; }
	public double MaxSpeed { get; set; }
	public double MaxTraction { get; set; }
	public double Acceleration { get; set; }
	public double Agility { get; set; }
	public double MaxKnots { get; set; }
	public double MoveResistance { get; set; }
	public bool HasArmoredWindows { get; set; }
	public List<DefaultColor>? DefaultColors { get; set; }
	public double DefaultBodyHealth { get; set; }
	public double DirtLevelMin { get; set; }
	public double DirtLevelMax { get; set; }
	public List<string?>? Trailers { get; set; }
	public List<string?>? AdditionalTrailers { get; set; }
	public List<int>? Extras { get; set; }
	public List<int>? RequiredExtras { get; set; }
	public double SpawnFrequency { get; set; }
	public int WheelsCount { get; set; }
	public bool HasParachute { get; set; }
	public bool HasKers { get; set; }
	public long DefaultHorn { get; set; }
	public int DefaultHornVariation { get; set; }
	public List<Bone>? Bones { get; set; }
}

[Serializable]
public class DisplayName
{
	public long Hash { get; set; }
	public string? English { get; set; }
	public string? German { get; set; }
	public string? French { get; set; }
	public string? Italian { get; set; }
	public string? Russian { get; set; }
	public string? Polish { get; set; }
	public string? Name { get; set; }
	public string? TraditionalChinese { get; set; }
	public string? SimplifiedChinese { get; set; }
	public string? Spanish { get; set; }
	public string? Japanese { get; set; }
	public string? Korean { get; set; }
	public string? Portuguese { get; set; }
	public string? Mexican { get; set; }
}

[Serializable]
public class ManufacturerDisplayName
{
	public long Hash { get; set; }
	public string? English { get; set; }
	public string? German { get; set; }
	public string? French { get; set; }
	public string? Italian { get; set; }
	public string? Russian { get; set; }
	public string? Polish { get; set; }
	public string? Name { get; set; }
	public string? TraditionalChinese { get; set; }
	public string? SimplifiedChinese { get; set; }
	public string? Spanish { get; set; }
	public string? Japanese { get; set; }
	public string? Korean { get; set; }
	public string? Portuguese { get; set; }
	public string? Mexican { get; set; }
}

[Serializable]
public class Dimensions
{
	public double X { get; set; }
	public double Y { get; set; }
	public double Z { get; set; }
}

[Serializable]
public class DefaultColor
{
	public int DefaultPrimaryColor { get; set; }
	public int DefaultSecondaryColor { get; set; }
	public int DefaultPearlColor { get; set; }
	public int DefaultWheelsColor { get; set; }
	public int DefaultInteriorColor { get; set; }
	public int DefaultDashboardColor { get; set; }
}

[Serializable]
public class Bone
{
	public int BoneIndex { get; set; }
	public long BoneId { get; set; }
	public string? BoneName { get; set; }
}
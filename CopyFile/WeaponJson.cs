[Serializable]
public class Tint
{
	public int Index { get; set; }
	public DisplayName? TranslatedLabel { get; set; }
}

[Serializable]
public class Components
{
	public string? Name { get; set; }
	public bool IsDefault { get; set; }

	public DisplayName? TranslatedLabel { get; set; }
	public DisplayName? TranslatedDescription { get; set; }
	public long Hash { get; set; }
	public int IntHash { get; set; }
	public string? AttachBone { get; set; }
	public string? Type { get; set; }
	public string? DlcName { get; set; }
}

[Serializable]
public class Variants
{
	public int Index { get; set; }
	public string? Name { get; set; }
}

[Serializable]
public class Liveries
{
	public string? Name { get; set; }
	public long Hash { get; set; }
	public int IntHash { get; set; }
	public DisplayName? TranslatedLabel { get; set; }
	public string? DlcName { get; set; }
}

[Serializable]
public class Weapon
{
	public string? Name { get; set; }
	public DisplayName? TranslatedLabel { get; set; }
	public long Hash { get; set; }
	public int IntHash { get; set; }
	public string? DlcName { get; set; }
	public string? Category { get; set; }
	public string? ModelName { get; set; }
	public string? AmmoType { get; set; }
	public string? AmmoModelName { get; set; }
	public int DefaultMaxAmmoSp { get; set; }
	public int SkillAbove50MaxAmmoSp { get; set; }
	public int MaxSkillMaxAmmoSp { get; set; }
	public int DefaultMaxAmmoMp { get; set; }
	public int SkillAbove50MaxAmmoMp { get; set; }
	public int MaxSkillMaxAmmoMp { get; set; }
	public int BonusMaxAmmoMp { get; set; }
	public string? DamageType { get; set; }
	public DisplayName? TranslatedDescription { get; set; }
	public List<Tint>? Tints { get; set; }
	public bool IsVehicleWeapon { get; set; }
	public List<string?>? Flags { get; set; }
	public List<Components?>? Components { get; set; }
	public List<Liveries?>? Liveries { get; set; }
}

public class BonusShuffle : BonusBase
{
	public override void Collect()
	{
		Action(this);
		Explode();
	}
}


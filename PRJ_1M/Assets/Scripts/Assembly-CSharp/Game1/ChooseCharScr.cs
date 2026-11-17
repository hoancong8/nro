using System;

namespace Game1
{
	public class ChooseCharScr : mScreen, IActionListener
	{
		public Command[] vc_players;

		public static PlayerData[] playerData;

		private int cf;

		private int[] cx = new int[2];

		private int focus;

		private int[] cy = new int[2];

		private int[] rectPanel = new int[4] { 0, 0, 300, 200 };

		private int offsetY = -35;

		private int offsetX = -35;

		private void initLayout()
		{
			int pw = 300;
			int ph = 200;
			if (GameCanvas.w > 0 && pw > GameCanvas.w - 20) pw = GameCanvas.w - 20;
			if (GameCanvas.h > 0 && ph > GameCanvas.h - 20) ph = GameCanvas.h - 20;
			rectPanel[0] = (GameCanvas.w - pw) / 2;
			rectPanel[1] = (GameCanvas.h - ph) / 2;
			rectPanel[2] = pw;
			rectPanel[3] = ph;
		}

		public override void switchToMe()
		{
			ServerListScreen.isWait = false;
			Char.isLoadingMap = false;
			LoginScr.isContinueToLogin = false;
			ServerListScreen.waitToLogin = false;
			GameScr.gI().initSelectChar();
			initLayout();
			base.switchToMe();
		}

		public override void update()
		{
			if (GameCanvas.gameTick % 10 > 2)
			{
				cf = 1;
			}
			else
			{
				cf = 0;
			}
			if (vc_players != null)
			{
				for (int i = 0; i < vc_players.Length; i++)
				{
					if (vc_players[i] != null && vc_players[i].isPointerPressInside())
					{
						vc_players[i].performAction();
					}
				}
			}
			if (cx != null)
			{
				for (int j = 0; j < cx.Length; j++)
				{
					if (GameCanvas.isPointerHoldIn(cx[j] + offsetX, cy[j] + offsetY, rectPanel[2], 60))
					{
						if (GameCanvas.isPointerDown)
						{
							focus = j;
							break;
						}
						if (GameCanvas.isPointerJustRelease)
						{
							_ = GameCanvas.isPointerClick;
						}
					}
				}
			}
			base.update();
		}

		public override void paint(mGraphics g)
		{
			GameCanvas.paintBGGameScr(g);
			try
			{
				if (rectPanel[0] <= 0 || rectPanel[1] <= 0 || rectPanel[0] + rectPanel[2] > GameCanvas.w || rectPanel[1] + rectPanel[3] > GameCanvas.h)
				{
					initLayout();
					if (playerData != null && playerData.Length > 0 && (cx == null || cx.Length != playerData.Length || cx[0] <= 0))
					{
						updateChooseCharacter((byte)playerData.Length);
					}
				}
				PopUp.paintPopUp(g, rectPanel[0] - 10, rectPanel[1], rectPanel[2] + 20, rectPanel[3], 16777215, isButton: true);
				if (vc_players != null)
				{
					for (int i = 0; i < vc_players.Length; i++)
					{
						if (vc_players[i] != null)
						{
							vc_players[i].paint(g);
						}
					}
				}
				if (playerData != null)
				{
					for (int j = 0; j < playerData.Length; j++)
					{
						if (cx != null && j < cx.Length && cy != null && j < cy.Length)
						{
							PopUp.paintPopUp(g, cx[j] - 20, cy[j] + offsetY, rectPanel[2], 60, 16777215, isButton: false);
							if (GameScr.parts != null)
							{
								if (playerData[j].head >= 0 && playerData[j].head < GameScr.parts.Length && GameScr.parts[playerData[j].head] != null && GameScr.parts[playerData[j].head].pi != null && GameScr.parts[playerData[j].head].pi.Length > Char.CharInfo[cf][0][0])
								{
									Part part = GameScr.parts[playerData[j].head];
									SmallImage.drawSmallImage(g, part.pi[Char.CharInfo[cf][0][0]].id, cx[j] + Char.CharInfo[cf][0][1] + part.pi[Char.CharInfo[cf][0][0]].dx, cy[j] - Char.CharInfo[cf][0][2] + part.pi[Char.CharInfo[cf][0][0]].dy, 0, 0);
								}
								if (playerData[j].leg >= 0 && playerData[j].leg < GameScr.parts.Length && GameScr.parts[playerData[j].leg] != null && GameScr.parts[playerData[j].leg].pi != null && GameScr.parts[playerData[j].leg].pi.Length > Char.CharInfo[cf][1][0])
								{
									Part part2 = GameScr.parts[playerData[j].leg];
									SmallImage.drawSmallImage(g, part2.pi[Char.CharInfo[cf][1][0]].id, cx[j] + Char.CharInfo[cf][1][1] + part2.pi[Char.CharInfo[cf][1][0]].dx, cy[j] - Char.CharInfo[cf][1][2] + part2.pi[Char.CharInfo[cf][1][0]].dy, 0, 0);
								}
								if (playerData[j].body >= 0 && playerData[j].body < GameScr.parts.Length && GameScr.parts[playerData[j].body] != null && GameScr.parts[playerData[j].body].pi != null && GameScr.parts[playerData[j].body].pi.Length > Char.CharInfo[cf][2][0])
								{
									Part part3 = GameScr.parts[playerData[j].body];
									SmallImage.drawSmallImage(g, part3.pi[Char.CharInfo[cf][2][0]].id, cx[j] + Char.CharInfo[cf][2][1] + part3.pi[Char.CharInfo[cf][2][0]].dx, cy[j] - Char.CharInfo[cf][2][2] + part3.pi[Char.CharInfo[cf][2][0]].dy, 0, 0);
								}
							}
							if (focus == j)
							{
								mFont.tahoma_7b_yellow.drawString(g, playerData[j].name, cx[j] + rectPanel[2] - 25, cy[j] + offsetY, 1);
								mFont.tahoma_7b_yellow.drawString(g, mResources.power_point + " " + Res.formatNumber2(playerData[j].powpoint), cx[j] + rectPanel[2] - 25, cy[j] + offsetY + mFont.tahoma_7b_yellow.getHeight(), 1);
							}
							else
							{
								mFont.tahoma_7b_dark.drawString(g, playerData[j].name, cx[j] + rectPanel[2] - 25, cy[j] + offsetY, 1);
								mFont.tahoma_7b_dark.drawString(g, mResources.power_point + " " + Res.formatNumber2(playerData[j].powpoint), cx[j] + rectPanel[2] - 25, cy[j] + offsetY + mFont.tahoma_7b_dark.getHeight(), 1);
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				Res.err("ERR ChooseCharScr.paint: " + ex);
			}
			base.paint(g);
		}

		internal void updateChooseCharacter(byte len)
		{
			initLayout();
			cx = new int[len];
			cy = new int[len];
			for (int i = 0; i < len; i++)
			{
				cx[i] = rectPanel[0] + 20;
				cy[i] = i * 70 + rectPanel[1] + 50;
			}
			vc_players = new Command[2];
			vc_players[1] = new Command("Vào game", this, 1, null, rectPanel[0] + rectPanel[2] - 80 - 80, rectPanel[1] + rectPanel[3] - 30);
			vc_players[0] = new Command("Trờ ra", this, 2, null, rectPanel[0] + rectPanel[2] - 80, rectPanel[1] + rectPanel[3] - 30);
		}

		public void perform(int idAction, object p)
		{
			switch (idAction)
			{
			case 2:
				GameCanvas.instance.doResetToLoginScr(GameCanvas.serverScreen);
				break;
			case 1:
				if (focus != -1)
				{
					GameCanvas.startWaitDlg();
					Service.gI().finishUpdate(playerData[focus].playerID);
				}
				break;
			}
		}
	}
}

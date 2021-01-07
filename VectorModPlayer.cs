using VectorMod.Buffs;
using VectorMod.Dusts;
using VectorMod.Items.BuffedLunarItems;
using VectorMod.Projectiles.BuffedLunarItems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.DataStructures;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace VectorMod
{
    public class VectorModPlayer : ModPlayer
    {
        //Hadridian Buffs
        public static bool CanGaze;
        public static bool CanPlateHusketB;
        public static bool CanRobeHusketB;
        public bool RobeGazeB;
        public bool HazedD;
        public bool PlateHusketB;
        public bool RobeHusketB;
        //Lunar Upgrade Buffs
        public bool EldritchD;
        public bool EldritchB;
        //Minion Buffs
        public bool HazerM;
        public bool StarSlicerM;

        public override void ResetEffects()
        {
            //Hadridian Buffs
            CanGaze = false;
            CanPlateHusketB = false;
            CanRobeHusketB = false;
            RobeGazeB = false;
            HazedD = false;
            PlateHusketB = false;
            RobeHusketB = false;
            //Lunar Upgrade Buffs
            EldritchD = false;
            EldritchB = false;
            //Minion Buffs
            HazerM = false;
            StarSlicerM = false;
        }

        public override void DrawEffects(PlayerDrawInfo drawInfo, ref float r, ref float g, ref float b, ref float a, ref bool fullBright)
        {
            if (HazedD)
            {
                if (Main.rand.NextBool(4) && drawInfo.shadow == 0f)
                {
                    int dust = Dust.NewDust(drawInfo.position - new Vector2(20f, 20f), player.width + 40, player.height + 40, DustType<HazeDust>(), player.velocity.X * 0.8f, player.velocity.Y * 0.8f, 100, default(Color), 3f);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].velocity.X = (player.Center.X - Main.dust[dust].position.X) / 10;
                    Main.dust[dust].velocity.Y = (player.Center.Y - Main.dust[dust].position.Y) / 10;
                    Main.playerDrawDust.Add(dust);
                }
                fullBright = true;
            }
        }

        public override void UpdateBadLifeRegen()
        {
            if (HazedD)
            {
                if (player.lifeRegen > 0)
                {
                    player.lifeRegen = 0;
                }
                player.allDamage -= 0.20f;
                player.lifeRegen -= 30;
            }
            if (EldritchD)
            {
                if (player.lifeRegen > 0)
                {
                    player.lifeRegen = 0;
                }
                player.lifeRegen -= 180;
            }
        }

        public override void UpdateDead()
        {
            //Hadridian Buffs
            RobeGazeB = false;
            HazedD = false;
            PlateHusketB = false;
            RobeHusketB = false;
            //Lunar Upgrade Buffs
            EldritchD = false;
            EldritchB = false;
        }

        public override void UpdateLifeRegen()
        {
            if (EldritchB)
            {
                player.lifeRegen += 10;
            }
            if (PlateHusketB)
            {
                player.statDefense += 12;
                player.endurance += 0.06f;
            }
            if (player.HasBuff(BuffID.Swiftness))
            {
                player.moveSpeed -= 0.05f;
            }
            if (RobeHusketB)
            {
                player.lifeRegen += 6;
                player.moveSpeed += 0.12f;
            }
        }

        static int GazeTimer = 0;
        static int GazeFrame = 0;
        public static readonly PlayerLayer HadGaze = new PlayerLayer("VectorMod", "HadGaze", PlayerLayer.Head, delegate (PlayerDrawInfo drawInfo)
        {
            Player player = drawInfo.drawPlayer;
            Color colorBase = Lighting.GetColor((int)(player.Center.X / 16), (int)(player.Center.Y / 16));
            int alphaOffset = (int)(Math.Abs(player.stealth - 1) * 255);
            Color color = new Color(colorBase.R - alphaOffset, colorBase.G - alphaOffset, colorBase.B - alphaOffset, colorBase.A - alphaOffset);
            SpriteEffects spriteEffect = (player.direction == -1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None) | (player.gravDir == -1 ? SpriteEffects.FlipVertically : SpriteEffects.None);

            Vector2 position = player.Center + new Vector2(0, -16);
            if (CanGaze)
            {
                GazeTimer++;
                if (GazeTimer >= 3)
                {
                    GazeFrame++;
                    GazeTimer = 0;
                }
                if (GazeFrame >= 4)
                {
                    GazeFrame = 0;
                }
                DrawGaze(GetTexture("VectorMod/Items/CultSet/HadGaze"), 0, 0, 0, color);
            }

            void DrawGaze(Texture2D texture, float horizontalOffset, float verticalOffset, float rotation, Color suppliedColor)
            {
                Rectangle rectangle = texture.Frame(1, 4, 0, GazeFrame);
                Vector2 drawPos = player.Center - new Vector2(0, 16);
                Vector2 origin = player.direction == -1 ? rectangle.Size() * 0.5f + new Vector2(horizontalOffset + player.direction + rectangle.Width * 0.5f, verticalOffset) : rectangle.Size() * 0.5f + new Vector2(-horizontalOffset + player.direction - rectangle.Width * 0.5f, verticalOffset);
                Main.playerDrawData.Add(new DrawData(texture, drawPos, rectangle, suppliedColor, rotation, origin, player.HeldItem.scale, spriteEffect, 0));
            }


        });

        static float xOffset = -10f, yOffset = 4f;
        static int BeterRockTimer = 0;
        public static readonly PlayerLayer HeldItem = new PlayerLayer("VectorMod", "HeldItem", PlayerLayer.HeldItem, delegate (PlayerDrawInfo drawInfo)
        {
            Player player = drawInfo.drawPlayer;
            Color colorBase = Lighting.GetColor((int)(player.Center.X / 16), (int)(player.Center.Y / 16));
            int alphaOffset = (int)(Math.Abs(player.stealth - 1) * 255);
            Color color = new Color(colorBase.R - alphaOffset, colorBase.G - alphaOffset, colorBase.B - alphaOffset, colorBase.A - alphaOffset);
            SpriteEffects spriteEffect = (player.direction == -1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None) | (player.gravDir == -1 ? SpriteEffects.FlipVertically : SpriteEffects.None);
            bool drawConditions = !player.dead && player.active && !player.invis && !player.frozen && !player.stoned && player.HeldItem.modItem is ModItem item && item.CanUseItem(player);

            Vector2 position = player.Center + player.DirectionTo(Main.MouseWorld) * 20;
            Vector2 velocity = player.DirectionTo(Main.MouseWorld);
            if (player.HeldItem.type == ItemType<BetterBeater>())
            {
                if (player.controlUseItem && player.HeldItem.modItem is ModItem beater && beater.CanUseItem(player))
                {
                    BeterRockTimer++;
                    if (BeterRockTimer >= 60)
                    {
                        for (int i = 1; i < 3; i++)
                        {
                            Projectile.NewProjectile(position + velocity * 40, velocity.RotatedBy(MathHelper.ToRadians(i * 8)) * 15, ProjectileType<BetterBeaterRock>(), 360, 10, player.whoAmI);
                            Projectile.NewProjectile(position + velocity * 40, velocity.RotatedBy(MathHelper.ToRadians(-i * 8)) * 15, ProjectileType<BetterBeaterRock>(), 360, 10, player.whoAmI);
                        }
                        Projectile.NewProjectile(position - velocity * 40, velocity, ProjectileType<BetterBeamer>(), 6000, 0, player.whoAmI);
                        Main.PlaySound(ModLoader.GetMod("VectorMod").GetLegacySoundSlot(SoundType.Custom, "Sounds/HeyBeter"));
                        BeterRockTimer = 0;
                    }
                }
            }

            void DrawHoldoutSimple(Texture2D texture, float horizontalOffset, float verticalOffset, float rotation, Color suppliedColor)
            {
                Rectangle rectangle = texture.Frame(1, 12, 0, BeterRockTimer / 5);
                Vector2 drawPos = (player.MountedCenter - Main.screenPosition + new Vector2(player.direction * horizontalOffset, player.gfxOffY + verticalOffset)).Floor();
                Vector2 origin = player.direction == -1 ? rectangle.Size() * 0.5f + new Vector2(horizontalOffset + player.direction + rectangle.Width * 0.5f, verticalOffset) : rectangle.Size() * 0.5f + new Vector2(-horizontalOffset + player.direction - rectangle.Width * 0.5f, verticalOffset);
                Main.playerDrawData.Add(new DrawData(texture, drawPos, rectangle, suppliedColor, rotation, origin, player.HeldItem.scale, spriteEffect, 0));
            }

            if (drawConditions)
            {
                if (player.HeldItem.type == ItemType<BetterBeater>() && player.controlUseItem)
                {
                    DrawHoldoutSimple(GetTexture("VectorMod/Items/BuffedLunarItems/BetterBeaterAnim"), xOffset, yOffset, player.itemRotation, color);
                    DrawHoldoutSimple(GetTexture("VectorMod/Items/BuffedLunarItems/BetterBeater_Glow"), xOffset, yOffset, player.itemRotation, Color.White);
                }
            }
        });

        public override void ModifyDrawLayers(List<PlayerLayer> layers)
        {
            HeldItem.visible = true;
            int index2 = layers.FindIndex(heldItem => heldItem == PlayerLayer.HeldItem);
            if (index2 != -1)
            {
                layers.Insert(index2, HeldItem);
            }
        }

        public override void OnHitNPCWithProj(Projectile proj, NPC target, int damage, float knockback, bool crit)
        {
            Player player = Main.player[proj.owner];
            if (player.HeldItem.melee && player.HeldItem.noMelee && target.CanBeChasedBy())
            {
                if (CanPlateHusketB)
                {
                    player.AddBuff(BuffType<PlateHusketB>(), 180);
                }
                if (CanRobeHusketB)
                {
                    player.AddBuff(BuffType<RobeHusketB>(), 180);
                }
            }
        }
    }
}
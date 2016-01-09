using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using PGMEBackend.Entities;
using System.Collections;

namespace PGMEBackend.GLControls
{
    class GLEntityEditor
    {
        Color rectColor;

        int width = 0;
        int height = 0;

        public EntityEditorTools tool = EntityEditorTools.None;

        public int mouseX = -1;
        public int mouseY = -1;
        public int endMouseX = -1;
        public int endMouseY = -1;

        public Color rectDefaultColor = Color.FromArgb(0, 255, 0);
        public Color rectPaintColor = Color.FromArgb(255, 0, 0);
        public Color rectSelectColor = Color.FromArgb(255, 255, 0);
        public Color npcBoundsColor = Color.Magenta;

        public Spritesheet entityTypes;

        public List<Entity> currentEntity;
        public int currentEntityType = 0;

        public GLEntityEditor(int w, int h)
        {
            width = w;
            height = h;
            GL.ClearColor(Color.Transparent);
            SetupViewport();
            rectColor = rectDefaultColor;
            entityTypes = Spritesheet.Load(Properties.Resources.Entities_16x16, 16, 16);
        }

        public static implicit operator bool (GLEntityEditor b)
        {
            return b != null;
        }

        private void SetupViewport()
        {
            GL.Viewport(0, 0, width, height); // Use all of the glControl painting area
        }

        public void Paint(int w, int h)
        {
            GL.Enable(EnableCap.Texture2D);
            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
            PreRender();

            width = w;
            height = h;
            SetupViewport();

            GL.ClearColor(Color.Transparent);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            var proj = OpenTK.Matrix4.CreateOrthographicOffCenter(0, width, height, 0, -1, 1);
            GL.LoadMatrix(ref proj);

            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();

            Render();

            GL.Disable(EnableCap.Texture2D);
            GL.Disable(EnableCap.Blend);
            /*
            var err = GL.GetError();
            if (err != ErrorCode.NoError)
                System.Windows.Forms.MessageBox.Show(err.ToString(), "OpenGL Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            */
        }

        private void PreRender()
        {
            var layout = Program.currentLayout;
            if (layout != null)
            {
                layout.RefreshChunks((Program.currentLayout.globalTileset != null) ? Program.currentLayout.globalTileset.tileSheets : null,
                            (Program.currentLayout.localTileset != null) ? Program.currentLayout.localTileset.tileSheets : null, 0, 0, 1);
            }
        }

        private void Render()
        {
            MapLayout layout = Program.currentLayout;
            if (layout != null)
            {
                layout.Draw((layout.globalTileset != null) ? layout.globalTileset.tileSheets : null,
                            (layout.localTileset != null) ? layout.localTileset.tileSheets : null, 0, 0, 1);
                Program.mainGUI.SetGLEntityEditorSize(layout.layoutWidth * 16, layout.layoutHeight * 16);

                width = layout.layoutWidth * 16;
                height = layout.layoutHeight * 16;

                if(Config.settings.ShowGrid)
                {
                    Surface.SetColor(Color.Black);
                    for(int i = 0; i < layout.layoutWidth; i++)
                        Surface.DrawLine(new double[] { i * 16, 0, i * 16, height });
                    for (int i = 0; i < layout.layoutHeight; i++)
                        Surface.DrawLine(new double[] { 0, i * 16, width, i * 16 });
                }

                if (Program.currentMap != null)
                {
                    foreach (NPC npc in Program.currentMap.NPCs)
                    {
                        entityTypes.Draw(0, npc.xPos * 16, npc.yPos * 16, 1, 192);
                    }

                    foreach (Warp warp in Program.currentMap.Warps)
                    {
                        entityTypes.Draw(1, warp.xPos * 16, warp.yPos * 16, 1, 192);
                    }

                    foreach (Trigger trigger in Program.currentMap.Triggers)
                    {
                        entityTypes.Draw(2, trigger.xPos * 16, trigger.yPos * 16, 1, 192);
                    }

                    foreach (Sign sign in Program.currentMap.Signs)
                    {
                        entityTypes.Draw(3, sign.xPos * 16, sign.yPos * 16, 1, 192);
                    }
                }

                if (currentEntity.Count == 1 && currentEntity[0] is NPC)
                    Surface.DrawOutlineRect((currentEntity[0].xPos - (currentEntity[0] as NPC).xBounds) * 16, (currentEntity[0].yPos - (currentEntity[0] as NPC).yBounds) * 16, (currentEntity[0] as NPC).xBounds * 32 + 16, (currentEntity[0] as NPC).yBounds * 32 + 16, npcBoundsColor);
                
                foreach (Entity ent in currentEntity)
                    Surface.DrawOutlineRect(ent.xPos * 16, ent.yPos * 16, 16, 16, rectPaintColor);
                
                if (tool == EntityEditorTools.RectSelect || (mouseX >= 0 && mouseY >= 0 && mouseX < layout.layoutWidth && mouseY < layout.layoutHeight))
                {
                    int x = mouseX * 16;
                    int y = mouseY * 16;
                    int endX = endMouseX * 16;
                    int endY = endMouseY * 16;
                    
                    if (endMouseX >= width / 16)
                        endX = ((width - 1) / 16) * 16;
                    if (endMouseY >= height / 16)
                        endY = ((height - 1) / 16) * 16;
                    
                    int w = x - endX;
                    int h = y - endY;
                    
                    Surface.DrawOutlineRect(endX + (w < 0 ? 16 : 0), endY + (h < 0 ? 16 : 0), w + (w >= 0 ? 16 : -16), h + (h >= 0 ? 16 : -16), rectColor);
                }
            }
        }

        bool mouseMoved = false;

        public void MouseMove(int x, int y)
        {
            int oldMouseX = mouseX;
            int oldMouseY = mouseY;

            mouseX = x / 16;
            mouseY = y / 16;
            /*
            if (mouseX >= width / 16)
                mouseX = (width - 1) / 16;
            if (mouseY >= height / 16)
                mouseY = (height - 1) / 16;
            */
            if (x < 0)
                mouseX--;
            if (y < 0)
                mouseY--;
            
            if (mouseX == oldMouseX && mouseY == oldMouseY)
                return;

            if ((tool == EntityEditorTools.SingleSelect || tool == EntityEditorTools.MultiSelect) && clickedOnEntity && currentEntity.Count != 0)
            {
                foreach (Entity ent in currentEntity)
                {
                    ent.xPos += (short)(mouseX - oldMouseX);
                    ent.yPos += (short)(mouseY - oldMouseY);
                    Program.isEdited = true;
                }

                if (currentEntity.Count == 1)
                    Program.mainGUI.LoadEntityView(currentEntity[0]);
                else if (currentEntity.Count > 1)
                    Program.mainGUI.MultipleEntitiesSelected();

                mouseMoved = true;
            }
            else if (tool == EntityEditorTools.CreateDelete)
            {
                rectColor = rectDefaultColor;
            }

            if (tool == EntityEditorTools.RectSelect)
            {
                List<Entity> newSelection = GetAllEntitiesWithinRect(mouseX, mouseY, endMouseX, endMouseY);

                if (newSelection.Count > 0)
                    currentEntity = newSelection;

                if (currentEntity.Count == 1)
                    Program.mainGUI.LoadEntityView(GetEntityType(currentEntity[0]), GetEntityNum(currentEntity[0]));
                else if (currentEntity.Count > 1)
                    Program.mainGUI.MultipleEntitiesSelected();
            }
            
            else
            {
                endMouseX = mouseX;
                endMouseY = mouseY;
            }
        }

        public List<Entity> GetAllEntitiesWithinRect(int startX, int startY, int endX, int endY)
        {
            List<Entity> entityList = new List<Entity>();
            foreach (NPC npc in Program.currentMap.NPCs)
            {
                if (IsEntityWithinBounds(npc, startX, startY, endX, endY))
                    entityList.Add(npc);
            }
            foreach (Warp warp in Program.currentMap.Warps)
            {
                if (IsEntityWithinBounds(warp, startX, startY, endX, endY))
                    entityList.Add(warp);
            }
            foreach (Trigger trigger in Program.currentMap.Triggers)
            {
                if (IsEntityWithinBounds(trigger, startX, startY, endX, endY))
                    entityList.Add(trigger);
            }
            foreach (Sign sign in Program.currentMap.Signs)
            {
                if (IsEntityWithinBounds(sign, startX, startY, endX, endY))
                    entityList.Add(sign);
            }
            return entityList;
        }

        public bool IsEntityWithinBounds(Entity entity, int x1, int y1, int x2, int y2)
        {
            if (entity.xPos >= (mouseX < endMouseX ? mouseX : endMouseX) && entity.yPos >= (mouseY < endMouseY ? mouseY : endMouseY) && entity.xPos <= (mouseX > endMouseX ? mouseX : endMouseX) && entity.yPos <= (mouseY > endMouseY ? mouseY : endMouseY))
                return true;
            return false;
        }

        public void MouseLeave()
        {
            mouseX = -1;
            mouseY = -1;
            endMouseX = -1;
            endMouseY = -1;
        }
        
        public Entity GetTopEntityFromPos(int x, int y)
        {
            foreach (Sign sign in Program.currentMap.Signs)
            {
                if (sign.xPos == mouseX && sign.yPos == mouseY)
                    return sign;
            }
            foreach (Trigger trigger in Program.currentMap.Triggers)
            {
                if (trigger.xPos == mouseX && trigger.yPos == mouseY)
                    return trigger;
            }
            foreach (Warp warp in Program.currentMap.Warps)
            {
                if (warp.xPos == mouseX && warp.yPos == mouseY)
                    return warp;
            }
            foreach (NPC npc in Program.currentMap.NPCs)
            {
                if (npc.xPos == mouseX && npc.yPos == mouseY)
                    return npc;
            }
            return null;
        }

        bool clickedOnEntity = false;

        public void MouseDown(EntityEditorTools Tool)
        {
            if (tool == EntityEditorTools.None && Program.currentMap != null)
            {
                tool = Tool;
                if (tool == EntityEditorTools.SingleSelect)
                {
                    Entity mouseOn = GetTopEntityFromPos(mouseX, mouseY);
                    if (mouseOn != null)
                    {
                        clickedOnEntity = true;
                        rectColor = rectSelectColor;
                        if (currentEntity.Count <= 1 || (currentEntity.Count > 1 && !currentEntity.Contains(mouseOn)))
                        {
                            currentEntity = new List<Entity> { mouseOn };
                            Program.mainGUI.LoadEntityView(GetEntityType(mouseOn), GetEntityNum(mouseOn));
                        }
                    }
                }
                else if (tool == EntityEditorTools.RectSelect)
                {
                    rectColor = rectSelectColor;
                    endMouseX = mouseX;
                    endMouseY = mouseY;
                    List<Entity> newSelection = GetAllEntitiesWithinRect(mouseX, mouseY, endMouseX, endMouseY);

                    if (newSelection.Count > 0)
                        currentEntity = newSelection;

                    if (currentEntity.Count == 1)
                        Program.mainGUI.LoadEntityView(GetEntityType(currentEntity[0]), GetEntityNum(currentEntity[0]));
                    else if (currentEntity.Count > 1)
                        Program.mainGUI.MultipleEntitiesSelected();
                }
                else if (tool == EntityEditorTools.MultiSelect)
                {
                    Entity mouseOn = GetTopEntityFromPos(mouseX, mouseY);
                    if (mouseOn != null)
                    {
                        rectColor = rectSelectColor;
                        if (!currentEntity.Remove(mouseOn))
                        {
                            clickedOnEntity = true;
                            currentEntity.Add(mouseOn);
                        }

                        if (currentEntity.Count == 1)
                            Program.mainGUI.LoadEntityView(GetEntityType(currentEntity[0]), GetEntityNum(currentEntity[0]));
                    }
                }
                else if (tool == EntityEditorTools.CreateDelete)
                {
                    rectColor = rectPaintColor;
                    Entity mouseOn = GetTopEntityFromPos(mouseX, mouseY);
                    if (mouseOn != null)
                    {
                        Program.mainGUI.DeleteEntity(mouseOn);
                    }
                    else
                        Program.mainGUI.CreateNewEntity(currentEntityType, mouseX, mouseY);
                }
                else
                    rectColor = rectDefaultColor;
            }
        }

        public static int GetEntityType(Entity entity)
        {
            if (entity is NPC)
                return 0;
            else if (entity is Warp)
                return 1;
            else if (entity is Trigger)
                return 2;
            else if (entity is Sign)
                return 3;
            return -1;
        }
        
        public static IList GetEntityList(Entity entity)
        {
            if (entity is NPC)
                return Program.currentMap.NPCs;
            else if (entity is Warp)
                return Program.currentMap.Warps;
            else if (entity is Trigger)
                return Program.currentMap.Triggers;
            else if (entity is Sign)
                return Program.currentMap.Signs;
            return null;
        }

        public static int GetEntityNum(Entity entity)
        {
            if (entity is NPC)
            {
                int i = 0;
                foreach(NPC npc in Program.currentMap.NPCs)
                {
                    if (npc == entity)
                        return i;
                    i++;
                }
            }
            else if (entity is Warp)
            {
                int i = 0;
                foreach (Warp warp in Program.currentMap.Warps)
                {
                    if (warp == entity)
                        return i;
                    i++;
                }
            }
            else if (entity is Trigger)
            {
                int i = 0;
                foreach (Trigger trigger in Program.currentMap.Triggers)
                {
                    if (trigger == entity)
                        return i;
                    i++;
                }
            }
            else if (entity is Sign)
            {
                int i = 0;
                foreach (Sign sign in Program.currentMap.Signs)
                {
                    if (sign == entity)
                        return i;
                    i++;
                }
            }
            return -1;
        }
        
        public void MouseUp(EntityEditorTools Tool)
        {
            if (tool == Tool)
            {
                if (tool == EntityEditorTools.SingleSelect && !mouseMoved)
                {
                    Entity mouseOn = GetTopEntityFromPos(mouseX, mouseY);
                    if (mouseOn != null)
                    {
                        currentEntity = new List<Entity> { mouseOn };
                        Program.mainGUI.LoadEntityView(GetEntityType(mouseOn), GetEntityNum(mouseOn));
                    }
                }
                else if (tool == EntityEditorTools.RectSelect)
                {
                    endMouseX = mouseX;
                    endMouseY = mouseY;
                }

                tool = EntityEditorTools.None;
                rectColor = rectDefaultColor;
                mouseMoved = false;
                clickedOnEntity = false;
            }
        }

        public void MouseDoubleClick()
        {
            Entity entity = GetTopEntityFromPos(mouseX, mouseY);
            if (entity != null)
            {
                if (entity is Warp)
                {
                    /*Program.mainGUI.LoadEntityView(1, (entity as Warp).destWarpNumber);
                    Warp.currentWarp = (entity as Warp).destWarpNumber;
                    Program.LoadMap((entity as Warp).destMapBank, (entity as Warp).destMapNum);*/
                    Program.mainGUI.FollowWarp(entity as Warp);
                }
                else
                {
                    Program.mainGUI.LaunchScriptEditor(entity.scriptOffset);
                }
            }
        }
    }

    public enum EntityEditorTools
    {
        None,
        SingleSelect,
        MultiSelect,
        RectSelect,
        CreateDelete
    }
}

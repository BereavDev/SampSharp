﻿using GameMode.Definitions;

namespace GameMode.World
{
    public class PlayerObject
    {
        #region Fields

        public const int InvalidId = Misc.InvalidObjectId;

        #endregion

        #region Properties

        public virtual Vector Position
        {
            get { return Native.GetPlayerObjectPos(Player.PlayerId, ObjectId); }
            set { Native.SetPlayerObjectPos(Player.PlayerId, ObjectId, value); }
        }

        public virtual Vector Rotation
        {
            get { return Native.GetPlayerObjectRot(Player.PlayerId, ObjectId); }
            set { Native.SetPlayerObjectRot(Player.PlayerId, ObjectId, value); }
        }

        public virtual bool IsMoving
        {
            get { return Native.IsPlayerObjectMoving(Player.PlayerId, ObjectId); }
        }

        public virtual bool IsValid
        {
            get { return Native.IsValidPlayerObject(Player.PlayerId, ObjectId); }
        }

        public virtual int ModelId { get; private set; }

        public virtual float DrawDistance { get; private set; }

        public virtual int ObjectId { get; private set; }

        public virtual Player Player { get; private set; }
        #endregion

        #region Constructor

        public PlayerObject(Player player, int modelid, Vector position, Vector rotation, float drawDistance)
        {
            Player = player;
            ModelId = modelid;
            DrawDistance = drawDistance;

            ObjectId = Native.CreatePlayerObject(player.PlayerId, modelid , position, rotation, drawDistance);
        }

        public PlayerObject(Player player,  int modelid, Vector position, Vector rotation)
           : this(player, modelid, position, rotation, 0)
        {

        }

        #endregion

        #region Methods

        public virtual void AttachTo(Player player, Vector offset, Vector rotation)
        {
            Native.AttachPlayerObjectToPlayer(Player.PlayerId, ObjectId, player.PlayerId, offset, rotation);
        }

        public virtual void AttachTo(Vehicle vehicle, Vector offset, Vector rotation)
        {
            Native.AttachPlayerObjectToVehicle(Player.PlayerId, ObjectId, vehicle.VehicleId, offset, rotation);
        }

        public virtual int Move(Vector position, float speed, Vector rotation)
        {
            return Native.MovePlayerObject(Player.PlayerId, ObjectId, position, speed, rotation);
        }

        public virtual int Move(Vector position, float speed)
        {
            return Native.MovePlayerObject(Player.PlayerId, ObjectId, position.X, position.Y, position.Z, speed, -1000, -1000, -1000);
        }

        public virtual void Stop()
        {
            Native.StopPlayerObject(Player.PlayerId, ObjectId);
        }

        public virtual void Edit()
        {
            Native.EditPlayerObject(Player.PlayerId, ObjectId);
        }

        public static void Select(Player player)
        {
            Native.SelectObject(player.PlayerId);
        }

        public virtual void SetMaterial(int materialindex, int modelid, string txdname, string texturename, Color materialcolor)
        {
            Native.SetPlayerObjectMaterial(Player.PlayerId, ObjectId, materialindex, modelid, txdname, texturename,
                materialcolor.GetColorValue(ColorFormat.ARGB));
        }

        public virtual void SetMaterialText(string text, int materialindex, ObjectMaterialSize materialsize,
            string fontface, int fontsize, bool bold, Color foreColor, Color backColor,
            ObjectMaterialTextAlign textalignment)
        {
            Native.SetPlayerObjectMaterialText(Player.PlayerId, ObjectId, text, materialindex, (int)materialsize, fontface, fontsize, bold,
                foreColor.GetColorValue(ColorFormat.ARGB), backColor.GetColorValue(ColorFormat.ARGB),
                (int)textalignment);
        }

        public virtual void Dispose()
        {
            Native.DestroyObject(ObjectId);
        }

        #endregion
    }
}
﻿using Daze.Geometry;
using System;
using System.Collections.Generic;

namespace Daze {
    public class GameObject:GameScript {
        #region variables
        #region variables for timers
        internal int lastTimerIndex = -1;

        #endregion

        #region variables for sprites
<<<<<<< HEAD
        internal Sprite lastSprite;
        private SpriteSet _SpriteSet;
        /// <summary>
        /// The SpriteSet for this gameObject
        /// </summary>
        public virtual SpriteSet spriteSet {
            get => _SpriteSet;
            set {
                value.reset();
                if(value == null) {
                    Engine.clean(this, false);
                }
=======
        private SpriteSet _SpriteSet;
        public SpriteSet spriteSet {
            get => _SpriteSet;
            set {
>>>>>>> 84a047f1bcbd99d313f202b4c6b43b160f16d8b1
                _SpriteSet = value;
                collider?.recreateCollider();
                pushPixelPosition();
            }
        }

        #endregion

        #region Variable for position
        //usate per il disegno su schermo
        internal protected IntVector lastPixelPosition;
        internal protected IntVector pixelPosition;

        /// <summary>
        /// The position of the gameObject
        /// </summary>
        public Point position;

        private float _Rotation = 0;
<<<<<<< HEAD
        /// <summary>
        /// The rotation of the gameObjects
        /// </summary>
=======
>>>>>>> 84a047f1bcbd99d313f202b4c6b43b160f16d8b1
        public float rotation {
            get => _Rotation;
            set {
                _Rotation = value;
<<<<<<< HEAD
                spriteSet?.rotate();
=======
>>>>>>> 84a047f1bcbd99d313f202b4c6b43b160f16d8b1
                collider?.rotateCollider();
            }
        }
        #endregion

        /// <summary>
        /// The position of the GameObject on the Z axis
        /// (note: the Z axis is Only used for drawing's priority)
        /// </summary>
        public int drawLayer;

        protected Collider _collider;
<<<<<<< HEAD
        /// <summary>
        /// The collider of this GameObject, colliders can have various shapes and adapt to the SpriteSet
        /// </summary>
        public virtual Collider collider { get => _collider;
            set { _collider = value; } }
=======
        public virtual Collider collider { get => _collider; set { _collider = value; } }
>>>>>>> 84a047f1bcbd99d313f202b4c6b43b160f16d8b1

        /// <summary>
        /// Returns the object that collided with this Object in the last movement, can be NULL
        /// </summary>
        public GameObject lastCollision { get { return _lastCollision; } }

        /// <summary>
        /// NOT RECOMMENDED: the list of the timers used by this GameObject.
        /// You should use the default functions related to timers for editing this, but...
        /// y'know, i don't really like closed codes in wich you can't fuck up everything, don't cha agree?
        /// </summary>
        protected internal List<Timer> timers;
        protected internal List<Timer> newTimers;
        protected internal List<Timer> toDeleteTimers;

        /// <summary>
        /// This contain the GameObject that collided with this one in the last movement (NULL if there was no collision)
        /// </summary>
        protected GameObject _lastCollision;
<<<<<<< HEAD
        /// <summary>
        /// The layers of colliders that this GameObjects should ignore while checking collisions
        /// </summary>
        public List<IgnoreLayer> ignoreLayers;
=======
>>>>>>> 84a047f1bcbd99d313f202b4c6b43b160f16d8b1
        #endregion

        #region Constructors
        /// <summary>
        /// Create a GameObject
        /// </summary>
        /// <param name="x">The position of the GameObject on the X axis</param>
        /// <param name="y">The position of the GameObject on the Y axis</param>
        /// <param name="drawLayer">The priority for drawing this Object</param>
        public GameObject(float x, float y, int drawLayer) {
            timers = new List<Timer>();
            newTimers = new List<Timer>();
            toDeleteTimers = new List<Timer>();

            //aggiungo il gameObject appena creato alla lista dei gameObject
            Engine.AddGameObject(this);

            //imposto la sua posizione
            position.x = x;
            position.y = y;

            _lastCollision = null;
<<<<<<< HEAD
            ignoreLayers = new List<IgnoreLayer>();
=======
>>>>>>> 84a047f1bcbd99d313f202b4c6b43b160f16d8b1

            //avvio il metodo di inizializzazione del gameObject
            Start();
            
            this.drawLayer = drawLayer;
        }

        /// <summary>
        /// Create a GameObject
        /// </summary>
        /// <param name="x">The position of the GameObject on the X axis</param>
        /// <param name="y">The position of the GameObject on the Y axis</param>
        public GameObject(float x, float y) : this(x, y, 0) { }
        #endregion

        #region Methods to use Engine timers
        /// <summary>
        /// This method create a timer that you can check in the Update method
        /// </summary>
        /// <param name="timerID">The ID of the timerw, different gameObject can have the same ID for different timer</param>
        /// <param name="msPerTick">The number of milliseconds necessary for this timer to tick</param>
        /// <param name="tickAction">The Action that will be executed when this Timer ticks
        /// <param name="currentMS">The current number of milliseconds passed from the last tick</param>
        public Timer createTimer(int timerID, int msPerTick, Action tickAction, int currentMS) {
            return createTimer(timerID, msPerTick, tickAction, true, currentMS);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="timerID">The ID of the timerw, different gameObject can have the same ID for different timer</param>
        /// <param name="msPerTick">The number of milliseconds necessary for this timer to tick</param>
        /// <param name="tickAction">The Action that will be executed when this Timer ticks
        /// <param name="restartFlag">If this flag is set to false then the Timer will not reset automatically once it ticks</param>
        /// <param name="currentMS">The current number of milliseconds passed from the last tick</param>
        /// <returns></returns>
        public Timer createTimer(int timerID, int msPerTick, Action tickAction, bool restartFlag = true, int currentMS = 0) {
            if(msPerTick < 1) throw new Exception("You cannot create a timer that tick every less than one second");
            //cerco il timer, se esiste cambio il tuo rateo di tick
            bool timerExists = false;
            foreach(Timer timer in timers) {
                if(timer.ID == timerID) {
                    timerExists = true;
                }
            }
            //se non esiste ne creo uno nuovo
            if(timerExists) {
                throw new Exception("A timer with the ID:" + timerID + " already exists");
            } else {
                Timer timer = new Timer(timerID, msPerTick, tickAction, restartFlag, currentMS);
                newTimers.Add(timer);
                return timer;
            }
        }

        /// <summary>
        /// It retrives a Timer in this GameObject from its ID
        /// </summary>
        /// <param name="timerID">The if of the Timer to search for</param>
        /// <returns>The searched Timer, can be NULL if there is no Timer with the specified ID</returns>
        public Timer getTimer(int timerID) {
            foreach(Timer timer in timers) {
                if(timer.ID == timerID) {
                    return timer;
                }
            }
            return null;
        }

        /// <summary>
        /// It deletes a Timer in this GameObject from its ID
        /// </summary>
        /// <param name="timerID">The if of the Timer to delete</param>
        /// <returns>Returns true if the timer was found and deleted, false otherwise</returns>
        public bool removeTimer(int timerID) {
            int index = -1;
            for(int i = 0; i < timers.Count && index == -1; i++) {
                if(timers[i].ID == timerID) {
                    index = i;
                    toDeleteTimers.Add(timers[i]);
                }
            }
            if(index != -1) {
                return true;
            }
            return false; ;
        }

        #endregion

        #region Methods for to the Draw function
        internal void pushLastPixelPosition() {
            lastPixelPosition.set(pixelPosition);
        }

        internal virtual void pushPixelPosition() {
            if(spriteSet != null) {
                pixelPosition.set((int)(position.x - spriteSet.size.width / 2) - Engine._camera.pixelPosition.x, (int)(position.y - spriteSet.size.height / 2) - Engine._camera.pixelPosition.y);
            }
        }
        #endregion

        #region Methods related to the movement of the GameObject
        /// <summary>
        /// This function moves the gameObject of a certain offset
        /// </summary>
        /// <param name="offset">The vector representing the movement of the gameObject</param>
        /// <returns></returns>
        public virtual bool move(Vector offset) {
            return move(offset.x, offset.y);
        }


        /// <summary>
        /// This function moves the gameObject of a certain offset
        /// </summary>
        /// <param name="xOffset">The offset of the movement of the GameObject on the X axis</param>
        /// <param name="yOffset">The offset of the movement of the GameObject on the Y axis</param>
        /// <returns>This return true if the gameObject moved without collisions, false otherwise</returns>
        public virtual bool move(float xOffset, float yOffset) {
            _lastCollision = null;
            bool moved = moveX(xOffset);
            moved = moveY(yOffset) && moved;
            return moved;
        }

        private bool moveX(float xOffset) {
            //muovo il GameObject
            position.x += xOffset;
<<<<<<< HEAD
            //controllo subito se il gameObject è limitato a muoversi nei bordi della camera
            if(Engine.camera.isFixed) {
                if((position.x + spriteSet?.size.width / 2) > Engine.camera.limits.maxX ||(position.x - spriteSet?.size.width / 2) < Engine.camera.limits.minX) {
                    //il gameObject è limitato ed è uscito dallo schermo, annullo il suo movimento
                    position.x -= xOffset;
                    _lastCollision = null;
                    return false;
                }
            }

=======
>>>>>>> 84a047f1bcbd99d313f202b4c6b43b160f16d8b1
            if(collider == null) {
                return true;
            } else {
                collider.moveCollider();
                if((_lastCollision = checkCollisions()) != null) {
                    extrapolate(xOffset, 0);

                    //eseguo la collisione di questo oggetto
                    OnCollisionEnter();
                    //se l'altro oggetto non è stato cancellato dalla collisone con quest'oggetto allora avvio anche la sua collisione
                    if(Engine.gameObjectExists(_lastCollision)) {
                        _lastCollision._lastCollision = this;
                        _lastCollision.OnCollisionEnter();
                    }
                    return false;
                }
                return true;
            }
        }
        private bool moveY(float yOffset) {
<<<<<<< HEAD
            //muovo il GameObject
=======
>>>>>>> 84a047f1bcbd99d313f202b4c6b43b160f16d8b1
            position.y += yOffset;
            //controllo subito se il gameObject è limitato a muoversi nei bordi della camera
            if(Engine.camera.isFixed) {
                if((position.y + spriteSet?.size.height/2) > Engine.camera.limits.maxY || (position.y - spriteSet?.size.height / 2) < Engine.camera.limits.minY) {
                    //il gameObject è limitato ed è uscito dallo schermo, annullo il suo movimento
                    position.y -= yOffset;
                    _lastCollision = null;
                    return false;
                }
            }
            if(collider == null) {
                return true;
            } else {
                collider.moveCollider();
                GameObject collision2;
                if((collision2 = checkCollisions()) != null) {
                    _lastCollision = collision2;
                    extrapolate(0, yOffset);

                    //eseguo la collisione di questo oggetto
                    OnCollisionEnter();
                    //se l'altro oggetto non è stato cancellato dalla collisone con quest'oggetto allora avvio anche la sua collisione
                    if(Engine.gameObjectExists(_lastCollision)) {
                        _lastCollision._lastCollision = this;
                        _lastCollision.OnCollisionEnter();
                    }
                    return false;
                }
                return true;
            }
        }
        #endregion

        #region Methods related to collisions
        //METODO CHE FA USCIRE UN OGGETTO DA UN ALTRO IN CASO DI COLLISIONE
        protected void extrapolate(float xOffset, float yOffset) {
            position -= new Vector(xOffset, yOffset);
        }

        /// <summary>
        /// Check if this object collides with some other objects.
        /// </summary>
        /// <returns>This method return ONLY the first gameObject found that collides, NOT ALL OF THEM.</returns>
        protected GameObject checkCollisions() {
            foreach(GameObject gameObject2 in Engine.findGameObjects()) {
                if(this.collide(gameObject2)) {
                    return gameObject2;
                }
            }
            return null;
        }

        private bool collide(GameObject gameObject2) {
            //non controllo la collisione con oggetti senza collider o con se stesso
            if(gameObject2.collider == null || gameObject2 == this) return false;
            //se sono qui significa che è un altro oggetto, quindi devo controllare se è da ignorare, e se non lo è devo controllare la collisione;
            foreach(IgnoreLayer ignoreLayer in ignoreLayers) {
                foreach(GameObject gameObjectToIgnore in ignoreLayer.gameObjects) {
                    if(gameObjectToIgnore == gameObject2) {
                        return false;
                    }
                }
                foreach(Type typeToIgnore in ignoreLayer.types) {
                    if(typeToIgnore == gameObject2.GetType()) {
                        return false;
                    }
                }
            }
            //se sono qui significa che è un altro oggetto e non è da ignorare, quindi controllo la collisione regolarment
            return collider.collide(gameObject2.collider);
        }


        #endregion

        #region Virtual Methods that should be overridden by the Game Programmer in his scripts
        /// <summary>
        /// This method is called every game cycle, here you should check your timers, input flags, process movements and so on.
        /// </summary>
        public virtual void Update() { }

        /// <summary>
        /// This method is called once after the Object is created and before the first Update.
        /// </summary>
        public virtual void Start() { }

        /// <summary>
        /// This method is called when this object is colliding with another Object, the collision data are in the lastCollision property
        /// </summary>
        public virtual void OnCollisionEnter() { }
        #endregion

        /// <summary>
        /// Deletes this gameObject, is just a call to the Engine.DeleteGameObject function
        /// </summary>
        public void delete() {
            Engine.DeleteGameObject(this);
        }
    }

}
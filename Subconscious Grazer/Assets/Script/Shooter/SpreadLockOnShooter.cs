﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;

public class SpreadLockOnShooter : BaseShooter {
    [Header("Spread shooter properties")]

    [SerializeField, Tooltip("The amount of bullet this shooter shoots out at once.")]
    private int bulletCount;

    [SerializeField, Tooltip("The target this shooter is locking on to.")]
    private Transform targetTransform;

    [SerializeField, Tooltip("How wide the shot will spread out.")]
    private float shotWideness;

    #region Property
    public int BulletCount {
        get {
            return bulletCount;
        }
        set {
            bulletCount = value;
        }
    }

    public float ShotWideness {
        get {
            return shotWideness;
        }

        set {
            shotWideness = value;
        }
    }

    public Transform TargetTransform {
        get {
            return targetTransform;
        }

        set {
            targetTransform = value;
        }
    }

    #endregion

    private void OnValidate() {
        // If the number of pellets to shoot is negative or zero.
        if (bulletCount < 0) {
            // Make it positive and warn.
            bulletCount = 1;
            Debug.LogWarning(gameObject.name + " : ArcShooter.cs :: pelletShot must be a positive value!");
        }
    }

    public override void Shoot() {

        float offSet = shotWideness / 2f;

        // For each pellet we have to shoot
        for (int i = 0; i < bulletCount; ++i) {

            // The angle to rotate after each shot.
            float anglePerShot = (shotWideness / (bulletCount + 1)) - offSet;

            // Get where to shoot at.
            Vector2 directionToShootAt = targetTransform.position - transform.position;
            // Angle correctly to create an arc.
            directionToShootAt = directionToShootAt.Rotate(anglePerShot - offSet);
            // Create the bullet.
            InitBullet(directionToShootAt.normalized);
        }
    }

    public void SwitchTarget(Transform newTarget) {
        targetTransform = newTarget;
    }
}

# Animation/Rig/Mocap Integration

## Open Standards & File Formats
- **glTF 2.0 (.glb/.gltf)**: Preferred runtime format; supports skinning and animation clips.
- **FBX**: Common DCC interchange format; keep as source for editing, then export to glTF.
- **BVH**: Lightweight mocap format; import and retarget before export.

## Datasets & Tooling
- **Mixamo**, **CMU mocap**, **Rokoko** (Studio/Marketplace) are common sources for mocap clips.
- **Blender** (or MotionBuilder) can import FBX/BVH, clean motion, retarget to your rig, and export glTF.

## Rig & Naming Conventions
- Use a consistent bind pose (T/A), and keep bone orientations stable across characters.
- Prefer `Root/Hips`, `Spine1/Spine2`, `Neck/Head`, `UpperArm_L`, `LowerArm_L`, `Hand_L` (same for `_R`)
- Name clips consistently (`Idle`, `Walk`, `Run`, `JumpStart`, `JumpLoop`, `JumpLand`).

## Retargeting & Blend Trees
- Retarget mocap to the game rig by mapping source bones to target bones and baking to the target skeleton.
- Preserve or strip **root motion** intentionally (export both in-place and root-motion variants when useful).
- Use blend trees for locomotion (Idle/Walk/Run) and additive layers for aim/look offsets.

## Frame Rates & Units
- Normalize to **30 or 60 fps** and resample source clips to a consistent frame rate.
- Maintain **meters** as the unit scale and document axis conventions (Y-up right-handed coordinate system: +X right, +Y up, +Z forward) when exporting.

## Mocap → In-Game Pipeline
1. Acquire BVH/FBX clips (Mixamo/CMU/Rokoko).
2. Clean and trim in Blender/MotionBuilder (remove jitter, fix foot sliding, loop where needed).
3. Retarget to the game rig and bake to the target skeleton.
4. Split clips (locomotion, actions) and decide root-motion vs in-place.
5. Export to glTF 2.0 for runtime (archive FBX/BVH sources for editing).
6. Import into the game, set up blend trees, and validate in gameplay.

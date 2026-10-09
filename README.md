# ALU 0x0A — Unity 360 Video Tour

An interactive 360° virtual campus tour built with Unity. Users can navigate between three locations — Outside View, Enterprise, and Food Court — using in-scene hotspot buttons with smooth fade transitions.

---

## Requirements

- Unity **6000.4.9f1** (Unity 6)
- Universal Render Pipeline (URP) 17.4.0
- XR Interaction Toolkit 3.4.1
- TextMeshPro (included via Unity packages)

---

## Getting Started

### 1. Clone the repository

```bash
git clone https://github.com/<your-username>/alu-0x0A-unity-360_video_tour.git
```

### 2. Open in Unity

1. Open **Unity Hub**
2. Click **Open > Add project from disk**
3. Select the cloned `alu-0x0A-unity-360_video_tour` folder
4. Make sure Unity version **6000.4.9f1** is installed — Unity Hub will prompt you if it isn't
5. Let Unity import all assets (first open may take a few minutes)

### 3. Add scenes to Build Settings

1. Go to **File > Build Profiles** (or **File > Build Settings** in older Unity versions)
2. Add the following scenes in order:
   - `Assets/Scenes/MainMenuScene.unity`
   - `Assets/Scenes/CustomCampusTourScene.unity`
   - `Assets/Scenes/IntranetTourScene.unity`

### 4. Fix the Food Court Inspector assignment (required)

The Food Court sphere must be manually linked to the TourManager:

1. Open `Assets/Scenes/CustomCampusTourScene.unity`
2. In the **Hierarchy**, select the **TourManager** GameObject
3. In the **Inspector**, find the `Food Court` field (currently empty)
4. Drag the **Food Court Sphere** GameObject from the Hierarchy into that field
5. Save the scene (**Ctrl+S**)

---

## Running the Project

### In the Editor

1. Open `Assets/Scenes/MainMenuScene.unity`
2. Press the **Play** button (▶) at the top of the Unity Editor
3. Use the Main Menu to navigate to the Custom Campus Tour or Intranet Tour

### Build & Run (Standalone)

1. Go to **File > Build Profiles**
2. Select your target platform (e.g., Windows)
3. Click **Build and Run**

---

## Controls

| Action | Input |
|---|---|
| Look around | Click and drag the mouse |
| Click a hotspot | Left-click on a hotspot button |

---

## Project Structure

```
Assets/
├── Scenes/
│   ├── MainMenuScene.unity       # Entry point
│   ├── CustomCampusTourScene.unity  # 360 tour with 3 locations
│   └── IntranetTourScene.unity
├── Scripts/
│   ├── TourManager.cs            # Controls which sphere is active
│   ├── FadeManager.cs            # Handles fade-in/out transitions
│   ├── CursorLook.cs             # Mouse-look camera control
│   ├── Hotspot.cs                # Hotspot billboard (faces camera)
│   ├── SwitchRooms.cs            # Alternative room-switching logic
│   └── sceneloader.cs            # Scene loading utility
├── Materials/                    # 360 sphere materials
├── Textures/                     # 360 image textures
└── Videos/                       # 360 video assets
```

---

## Scenes Overview

- **MainMenuScene** — Entry screen with buttons to launch each tour
- **CustomCampusTourScene** — Three 360° spheres (Outside View, Enterprise, Food Court) with hotspot navigation and fade transitions
- **IntranetTourScene** — Secondary tour experience

---

## Troubleshooting

**Hotspot click does nothing**
- Ensure the scene has an **EventSystem** in the Hierarchy
- Check that the Camera is tagged as `MainCamera`

**Food Court doesn't deactivate when navigating away**
- The `Food Court` field on the **TourManager** component is not assigned — follow step 4 in [Getting Started](#getting-started)

**Scene not found error in console**
- The scene hasn't been added to Build Settings — follow step 3 in [Getting Started](#getting-started)

**Black screen / no fade**
- Verify the **FadeManager** has a `Fade Image` assigned in the Inspector

# ribbon 🎗️

**A light-weight Minecraft mod pack CLI Manager.**

This tool is meant to simplify creating mod packs by adding versioned mods and resolving their dependencies.

## How to use

### Configure

It is crucial to set your **gameVersion** and **modLoader**. This can be done using the *configure* command.

```
ribbon configure gameVersion 1.20.1
```
<sub>Note: `gameVersion` is not validated -- entering an incorrect game version will cause the program to not return any mods, regardless if the searched mod exists or not.</sub>


```
ribbon configure modLoader Forge
```
<sub>Note: `modLoader` is case-insensitive</sub>

### Mod Management

#### Add

Adding mods is easy. Use `ribbon add` with a mod Id or Name (`-n`).

```
ribbon add amendments -n
```

#### Clear

Deletes all mods. Use `ribbon clear`

#### List

List see current mods. Use `ribbon list`

## Roadmap

- Including other mod repositories -- CurseForge mods are the only source for now.
- Distinguished client/server mods -- would be useful to create a mirrored mod pack meant to be deployed server-side (removing client-side only mods).



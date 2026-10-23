@echo off
if not exist "C:\Users\qemu\.ssh" mkdir "C:\Users\qemu\.ssh"
echo ssh-ed25519 AAAAC3NzaC1lZDI1NTE5AAAAINJaQXEvJVSABsLFMy226p1hvJ9nK0Xrb//MLT+wrzPx fulanito@asus> "C:\ProgramData\ssh\administrators_authorized_keys"
echo ssh-ed25519 AAAAC3NzaC1lZDI1NTE5AAAAINJaQXEvJVSABsLFMy226p1hvJ9nK0Xrb//MLT+wrzPx fulanito@asus> "C:\Users\qemu\.ssh\authorized_keys"

icacls "C:\ProgramData\ssh\administrators_authorized_keys" /inheritance:r /grant SYSTEM:(F) /grant Administradores:(F)
icacls "C:\Users\qemu\.ssh" /inheritance:r /grant SYSTEM:(F) /grant Administradores:(F) /grant qemu:(F)
icacls "C:\Users\qemu\.ssh\authorized_keys" /inheritance:r /grant SYSTEM:(F) /grant Administradores:(F) /grant qemu:(F)

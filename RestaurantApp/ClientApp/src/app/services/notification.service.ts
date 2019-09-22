import { MatSnackBar, MatSnackBarConfig } from '@angular/material';
import { Injectable } from '@angular/core';

@Injectable({
providedIn: 'root'})
export class NotificationService {

  constructor(private snackBar: MatSnackBar) { }

  config: MatSnackBarConfig = {
    duration: 3000,
    horizontalPosition: 'right',
    verticalPosition: 'top',
    panelClass: ['notification', 'success']
  }

  success(msg: string) {
    this.config.panelClass = ['notification', 'success'];
      this.snackBar.open(msg, '', this.config);
  }

  warn(msg: string) {
    this.config.panelClass = ['notification', 'warn'];
    this.snackBar.open(msg, '', this.config);
  }
}

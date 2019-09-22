import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import * as Material from '@angular/material';

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    Material.MatButtonModule,
    Material.MatCardModule,
    Material.MatSelectModule,
    Material.MatFormFieldModule,
    Material.MatInputModule,
    Material.MatSnackBarModule
  ],
  exports: [
    Material.MatButtonModule,
    Material.MatCardModule,
    Material.MatSelectModule,
    Material.MatFormFieldModule,
    Material.MatInputModule,
    Material.MatSnackBarModule
  ]
})
export class MaterialModule { }

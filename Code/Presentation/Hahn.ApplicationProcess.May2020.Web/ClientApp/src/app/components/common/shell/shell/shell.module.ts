import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TranslateModule } from '@ngx-translate/core';
import { RouterModule } from '@angular/router';
import { NgxSpinnerModule } from "ngx-spinner";
import { ShellComponent } from './shell.component';

@NgModule({
  imports: [CommonModule, TranslateModule, RouterModule, NgxSpinnerModule],
  declarations: [ShellComponent]
})
export class ShellModule { }

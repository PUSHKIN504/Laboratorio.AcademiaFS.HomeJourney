import { Component, Input, Output, EventEmitter, OnInit, OnChanges, SimpleChanges, input, output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';
import { NgFor, NgIf } from '@angular/common';
import { CommonModule } from '@angular/common';
import { DxButtonModule, DxCheckBoxModule, DxPopupModule, DxScrollViewModule, DxSelectBoxModule } from 'devextreme-angular';
import { InputType } from 'zlib';
@Component({
  selector: 'app-custom-form',
  imports: [ReactiveFormsModule, CommonModule,  DxButtonModule,DxCheckBoxModule, DxSelectBoxModule, DxPopupModule,DxScrollViewModule],
  templateUrl: './custom-popup.component.html',
  styleUrls: ['./custom-popup.component.scss'],
  standalone: true,
})
export class CustomForm  {


title= input<string>('title'); 
isVisible= input<boolean>(false);

@Output() onHidden = new EventEmitter<any>();
}

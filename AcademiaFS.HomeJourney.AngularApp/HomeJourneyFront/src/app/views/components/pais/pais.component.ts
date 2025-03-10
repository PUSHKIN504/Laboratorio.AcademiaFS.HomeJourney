import { Component } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { DxFormModule, DxButtonModule, DxListModule } from 'devextreme-angular';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatCardModule } from '@angular/material/card';
import { PaisesDto } from '../../models/pais.model';

@Component({
  selector: 'app-pais',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    DxFormModule,
    DxButtonModule,
    DxListModule,
    MatToolbarModule,
    MatCardModule
  ],
  templateUrl: './pais.component.html',
  styleUrls: ['./pais.component.scss']
})
export class PaisComponent {
  paisForm: FormGroup;
  paises: PaisesDto[] = [
    { paisId: 1, nombre: 'México', activo: true },
    { paisId: 2, nombre: 'Argentina', activo: false },
    { paisId: 3, nombre: 'Colombia', activo: true }
  ];

  constructor(private fb: FormBuilder) {
    this.paisForm = this.fb.group({
      paisId: [null, Validators.required],
      nombre: ['', Validators.required],
      activo: [false, Validators.required]
    });
  }

  onFieldDataChanged(e: any): void {
    this.paisForm.patchValue({ [e.dataField]: e.value });
  }

  filtrarPaisesActivos(paises: PaisesDto[]): PaisesDto[] {
    return paises.filter(({ activo }) => activo);
  }

  onSubmit(): void {
    if (this.paisForm.valid) {
      console.log('Formulario enviado:', this.paisForm.value);
    } else {
      console.log('Formulario inválido');
    }
  }
}

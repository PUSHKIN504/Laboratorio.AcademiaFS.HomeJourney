import { Component, CUSTOM_ELEMENTS_SCHEMA, OnInit } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ConfigurationComponent } from '../../../components/configuration.component';
import { Colaborador, CreatePersonaColaboradorDto } from '../../../models/colaborador.model';
import { ConfigurationBaseService } from '../../../services/configuration-base.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { GoogleMapsModule } from '@angular/google-maps';
import {
  DxDataGridModule,
  DxFormModule,
  DxButtonModule,
  DxSelectBoxModule,
  DxNumberBoxModule,
  DxCheckBoxModule,
  DxTextBoxModule,
  DxPopupModule,
  DxScrollViewModule
} from 'devextreme-angular';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { CommonModule, JsonPipe } from '@angular/common';
import { CustomForm } from "../../../../shared/custom-popup/custom-popup.component";
import { ValidationPatterns } from '../../../../shared/validators/ValidationPatterns';

@Component({
  templateUrl: './colaboradores.component.html',
  styleUrls: ['./colaboradores.component.scss'],
  standalone: true,
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
  imports: [
    DxDataGridModule,
    DxFormModule,
    CustomForm,
    ReactiveFormsModule,
    DxPopupModule,
    DxScrollViewModule,
    DxButtonModule,
    DxSelectBoxModule,
    DxNumberBoxModule,
    DxCheckBoxModule,
    DxTextBoxModule,
    MatToolbarModule,
    MatCardModule,
    MatIconModule,
    MatButtonModule,
    CommonModule,
    GoogleMapsModule
  ]
})
export class ColaboradoresComponent extends ConfigurationComponent<CreatePersonaColaboradorDto> implements OnInit {

  personas = [
    { id: 1, nombre: 'Juan Pérez' },
    { id: 2, nombre: 'María Gómez' }
  ];
  roles = [
    { rolId: 1, nombre: 'Administrador' },
    { rolId: 2, nombre: 'Colaborador' }
  ];
  ciudades = [
    { ciudadId: 1, nombre: 'San Pedro Sula' },
    { ciudadId: 2, nombre: 'La Lima' }
  ];
  cargos = [
    { cargoId: 1, nombre: 'Gerente' },
    { cargoId: 2, nombre: 'Asistente' }
  ];
  estadociviles = [
    { estadocivilId: 1, nombre: 'Soltero' },
    { estadocivilId: 2, nombre: 'Casado' }
  ];

  mapCenter: google.maps.LatLngLiteral = { lat: 15.5000, lng: -88.0333 }; 
  mapZoom: number = 12;
  markerPosition?: google.maps.LatLngLiteral;

  calculateFullName(data: any): string {
    const nombre = JSON.stringify(data.nombre);
    const apellido = JSON.stringify(data.apellido);
    return `${nombre.slice(1, -1)} ${apellido.slice(1, -1)}`;
  }
  constructor(snackBar: MatSnackBar) {
    super("academiafarsiman/personascolaboradores", "Colaborador", snackBar);
  }

  override onInitForm(): void {
    this._form = new FormGroup({
      colaboradorId: new FormControl(0),
      nombre: new FormControl(null, [Validators.required]),
      apelllido: new FormControl(null, [Validators.required]),
      sexo: new FormControl(null, [Validators.required]),
      email: new FormControl(null, [Validators.required, Validators.email]),
      documentonacionalidentificacion: new FormControl(null, [
        Validators.required,
        Validators.pattern(ValidationPatterns.NUMERIC_ONLY)
      ]),
      ciudadId: new FormControl(null, [Validators.required]),
      estadocivilId: new FormControl(null, [Validators.required]),
      usuariocrea: new FormControl(1, [Validators.required]),
      rolId: new FormControl(null, [Validators.required]),
      cargoId: new FormControl(null, [Validators.required]),
      direccion: new FormControl(null, [Validators.required, Validators.maxLength(500)]),
      activo: new FormControl(true, [Validators.required]),
      latitud: new FormControl(null, [Validators.required]),
      longitud: new FormControl(null, [Validators.required])
    });
  }



onMapClick(event: google.maps.MapMouseEvent): void {
  if (!event.latLng) return;
  const lat = event.latLng.lat();
  const lng = event.latLng.lng();

  this._form.patchValue({
    latitud: lat,
    longitud: lng
  });

  this.markerPosition = { lat, lng };

  const geocoder = new google.maps.Geocoder();
  const latlngObj = { lat, lng };

  geocoder.geocode({ location: latlngObj }, (results, status) => {
    if (status === 'OK' && results && results.length > 0) {
      const formattedAddress = results[0].formatted_address;
      this._form.patchValue({ direccion: formattedAddress });

      const matchingCity = this.ciudades.find(city =>
        formattedAddress.toLowerCase().includes(city.nombre.toLowerCase())
      );

      if (matchingCity) {
        this._form.patchValue({ ciudadId: matchingCity.ciudadId });
        if (matchingCity.nombre.toLowerCase() === 'san pedro sula') {
          this.mapCenter = { lat, lng };
        }
      } else {
        const defaultCity = this.ciudades.find(city => city.nombre.toLowerCase() === 'san pedro sula');
        if (defaultCity) {
          this._form.patchValue({ ciudadId: defaultCity.ciudadId });
          this.mapCenter = { lat, lng };
        }
      }
    } else {
      console.error('Geocoder failed due to: ' + status);
    }
  });
}

  ngOnInit(): void {
    super.get(true);
  }
}

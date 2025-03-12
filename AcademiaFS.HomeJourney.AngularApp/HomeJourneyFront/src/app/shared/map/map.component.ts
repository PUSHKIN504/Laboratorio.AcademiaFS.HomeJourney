import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { tileLayer, latLng, Map, LeafletMouseEvent } from 'leaflet';
import { LeafletModule } from '@asymmetrik/ngx-leaflet';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';

@Component({
  selector: 'app-map',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    CommonModule,
    LeafletModule,
  ],
  template: `
    <div leaflet 
         [leafletOptions]="options"
         (leafletClick)="onMapClick($event)"
         style="height:300px;">
    </div>
  `,
  styles: []
})
export class MapComponent implements OnInit {
  @Output() locationSelected = new EventEmitter<{ lat: number; lng: number }>();

  options: any;

  ngOnInit(): void {
    this.options = {
      layers: [
        tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
          maxZoom: 18,
          attribution: '© OpenStreetMap contributors'
        })
      ],
      zoom: 13,
      center: latLng(51.505, -0.09)
    };
  }

  onMapClick(event: LeafletMouseEvent): void {
    this.locationSelected.emit({ lat: event.latlng.lat, lng: event.latlng.lng });
  }
}

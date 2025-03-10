import { ComponentFixture, TestBed } from '@angular/core/testing';
import { TestPageComponent } from './test-page.component';
import { DxDataGridModule } from 'devextreme-angular';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';

describe('TestPageComponent', () => {
  let component: TestPageComponent;
  let fixture: ComponentFixture<TestPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [TestPageComponent],
      imports: [
        NoopAnimationsModule,  // si usas animaciones
        DxDataGridModule,
        // Otros módulos necesarios si aplica
      ]
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TestPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create the component', () => {
    expect(component).toBeTruthy();
  });
});

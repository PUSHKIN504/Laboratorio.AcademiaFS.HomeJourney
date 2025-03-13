import { HttpErrorResponse } from '@angular/common/http';
import { FormControl, FormGroup, UntypedFormGroup, Validators } from "@angular/forms";
import { MatSnackBar } from '@angular/material/snack-bar';
import { ConfigurationBaseService } from "../services/configuration-base.service";
import Swal from 'sweetalert2';
import { JsonPipe } from '@angular/common';
export class ConfigurationComponent<TEntity> {
    maxHeight: number = window.screen.height * 0.69;
    text: string = "Configuracion";

    protected _form!: FormGroup; 

    protected listEntities: Array<TEntity> = [];

    popupOptions: { title: string, visible: boolean, loading: boolean } = {
        title: '', visible: false, loading: false
    };

    constructor(url: string, text: string, private snackBar: MatSnackBar) {
        this._baseService = new ConfigurationBaseService<TEntity>(url);
        this.text = text;
        this.onInitForm();
    }

    protected _baseService: ConfigurationBaseService<TEntity>;

    onInitForm(): void {
        this._form = new FormGroup({
            usuariocrea: new FormControl<number | null>(0),
            descripcion: new FormControl<string | null>(null, [Validators.required, Validators.maxLength(50)]),
            estaActivo: new FormControl<boolean | null>(true, [Validators.required])
        });
    }

    get(loading: boolean = false): void {
        if (loading) {
            this.snackBar.open('Cargando...', 'Cerrar', { duration: 3000 });
        }
        this._baseService.get()
            .then(response => this.listEntities = response.data)
            .catch(error => this.snackBar.open(String(error), 'Cerrar', { duration: 3000 }))
            .finally(() => {
                if (loading) {
                    setTimeout(() => this.snackBar.dismiss(), 1500);
                }
            });
    }
   
    onSave(): void {
      this.popupOptions.loading = true;
      this.popupOptions.visible = false;
      console.log(this._form);
      if (this._form.invalid) {
        Object.keys(this._form.controls).forEach(controlName => {
          this._form.get(controlName)?.markAsTouched();
        });
        Swal.fire({
          title: 'Error',
          text: 'Por favor, complete los campos requeridos',
          icon: 'warning',
          confirmButtonText: 'Cerrar'
        }).then(() => {
          this.popupOptions.visible = true;
          this.popupOptions.loading = false;
        });
        return;
      }
    
      const { colaboradorSucursalId } = this._form.value;
      const promise = colaboradorSucursalId == 0
      // const { colaboradorId } = this._form.value;
      // const promise = colaboradorId == 0
        ? this._baseService.add(<TEntity>this._form.value)
        : this._baseService.update(<TEntity>this._form.value);
    
      promise.then(data => {
        this.get();
        this.onClosePopup();
    
        Swal.fire({
          title: '¡Éxito!',
          text: String(Object.values(data)[1]),
          icon: 'success',
          confirmButtonText: 'Cerrar',
          customClass: {
            popup: 'swal2-popup-custom'
          }
        }).then(() => {
          this.popupOptions.loading = false;
        });
      }).catch(error => {
        Swal.fire({
          title: 'Error',
          text: `Error al guardar, ${error.error.message}` ,
          icon: 'error',
          confirmButtonText: 'Cerrar'
        }).then(() => {
          this.popupOptions.visible = true;
          this.popupOptions.loading = false;
        });
      });
    }
    onChangeStatus(event: any): void {
        const { data } = event;
        if (!data) return;

        const { usuariocrea, descripcion, estaActivo } = data;

        this._baseService.updateState(usuariocrea, estaActivo)
            .then(() => {
                this.get();
                this.snackBar.open(`Cambio de estado realizado a ${descripcion}`, 'Cerrar', { duration: 3000 });
            })
            .catch(error => this.snackBar.open(String(error), 'Cerrar', { duration: 3000 }));
    }

    onEditShow = (event: any): void => {
        this.popupOptions.title = "Agregar " + this.text.toLocaleLowerCase();

        if (!event) {
            this.popupOptions.visible = true;
            return;
        }
        const { data } = event.row;
        this.popupOptions.title = "Editar " + this.text.toLocaleLowerCase();
        console.log(data);
        this._form.patchValue(data);

        this.popupOptions.visible = true;
    }

    onClosePopup(): void {
        console.log(this.popupOptions.title);
        this.popupOptions = {
            title: '', visible: false, loading: false
        };
        this.onInitForm();

        this._form.markAsPristine();
        this._form.markAsUntouched();

    }

    calculateFilterExpression(filterValue: any, selectedFilterOperation: any, target: any) {
        //@ts-ignore
        const prop: string = this['dataField'];

        var getter = (data: any) => data[prop] === null ? "" : data[prop].normalize('NFD').replace(/[\u0300-\u036f]/g, "");
        filterValue = filterValue.normalize('NFD').replace(/[\u0300-\u036f]/g, "");

        return [getter, selectedFilterOperation || "contains", filterValue];
    }

    onRowUpdating(event: any): void {
        const { newData, oldData } = event;

        const propertyNames = Object.getOwnPropertyNames(newData);
        const { descripcion, usuariocrea } = oldData;

       this._baseService.updateEstado(oldData.usuariocrea, propertyNames[0], newData[propertyNames[0]])
       .then(() => {
           this.get();
           this.snackBar.open(`Cambio de estado realizado a ${descripcion}`, 'Cerrar', { duration: 3000 });
       })
       .catch(error => this.snackBar.open(String(error), 'Cerrar', { duration: 3000 }));
    }
}

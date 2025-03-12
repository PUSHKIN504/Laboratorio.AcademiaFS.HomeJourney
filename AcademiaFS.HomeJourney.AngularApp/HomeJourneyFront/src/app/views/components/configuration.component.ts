import { HttpErrorResponse } from '@angular/common/http';
import { FormControl, FormGroup, UntypedFormGroup, Validators } from "@angular/forms";
import { MatSnackBar } from '@angular/material/snack-bar';
import { ConfigurationBaseService } from "../services/configuration-base.service";

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

    // onSave(): void {
    //     this.popupOptions.loading = true;
    //     const { usuariocrea } = this._form.value;

    //     const promise = usuariocrea == 0
    //         ? this._baseService.add(<TEntity>this._form.value)
    //         : this._baseService.update(<TEntity>this._form.value);

    //     promise.then(data => {
    //         this.get();
    //         this.onClosePopup();
    //         this.snackBar.open(String(Object.values(data)[0]), 'Cerrar', { duration: 3000 });
    //         this.onClosePopup();
    //     }).catch(error => {
    //         this.snackBar.open(String(error), 'Cerrar', { duration: 3000 });
    //         this.popupOptions.loading = false;
    //     });
    // }
    onSave(): void {
        this.popupOptions.loading = true;
        const { colaboradorId } = this._form.value;  // Usamos colaboradorId en lugar de usuariocrea
    
        const promise = colaboradorId == 0
            ? this._baseService.add(<TEntity>this._form.value)
            : this._baseService.update(<TEntity>this._form.value);
    
        promise.then(data => {
            this.get();
            this.onClosePopup();
            this.snackBar.open(String(Object.values(data)[0]), 'Cerrar', { duration: 3000 });
            this.onClosePopup();
        }).catch(error => {
            this.snackBar.open(String(error), 'Cerrar', { duration: 3000 });
            this.popupOptions.loading = false;
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

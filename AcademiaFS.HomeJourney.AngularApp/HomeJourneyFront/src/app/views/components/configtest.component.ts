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
    // onSave(): void {
    //     this.popupOptions.loading = true;
    //     const { colaboradorId } = this._form.value;  // Usamos colaboradorId en lugar de usuariocrea
    
    //     const promise = colaboradorId == 0
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


    // onSave(): void {
    //     this.popupOptions.loading = true;
    //     this.popupOptions.visible = false;
    //     const { colaboradorId } = this._form.value; 
      
    //     const promise = colaboradorId == 0
    //         ? this._baseService.add(<TEntity>this._form.value)
    //         : this._baseService.update(<TEntity>this._form.value);
      
    //     promise.then(data => {
    //         this.get();
    //         this.onClosePopup();
    //         Swal.fire({
    //           title: '¡Éxito!',
    //           text: String(Object.values(data)[0]),
    //           icon: 'success',
    //           confirmButtonText: 'Cerrar',
    //           customClass: {
    //             popup: 'swal2-popup-custom'
    //           }
              
    //         });
    //         this.onClosePopup();
    //         this.popupOptions.visible = true;

    //     }).catch(error => {
    //         Swal.fire({
    //           title: 'Error',
    //           text: String(error),
    //           icon: 'error',
    //           confirmButtonText: 'Cerrar'
    //         });
    //         this.popupOptions.loading = false;
    //         this.popupOptions.visible = true;

    //     });
    //   }
    
    // onSave(): void {
    //     this.popupOptions.loading = true;
    //     this.popupOptions.visible = false;
    //     const { colaboradorId } = this._form.value; 
    //     if (this._form.invalid) {
    //         console.log(this._form);
    //         Swal.fire({
    //             title: 'Error',
    //             text: 'Por favor, complete los campos requeridos',
    //             icon: 'warning',
    //             confirmButtonText: 'Cerrar'
    //           }).then(() => {
    //             this.popupOptions.visible = true;
    //             this.popupOptions.loading = false;
    //           });
    //       return;
    //     }
    //     const promise = colaboradorId == 0
    //       ? this._baseService.add(<TEntity>this._form.value)
    //       : this._baseService.update(<TEntity>this._form.value);
      
    //     promise.then(data => {
    //       this.get();
    //       this.onClosePopup(); 
      
    //       Swal.fire({
    //         title: '¡Éxito!',
    //         text: String(Object.values(data)[0]),
    //         icon: 'success',
    //         confirmButtonText: 'Cerrar',
    //         customClass: {
    //           popup: 'swal2-popup-custom'
    //         }
    //       }).then(() => {
    //         this.popupOptions.loading = false;
    //       });
    //     }).catch(error => {
    //       Swal.fire({
    //         title: 'Error',
    //         text: 'Error al guardar, comunicarse con soporte tecnico,',
    //         icon: 'error',
    //         confirmButtonText: 'Cerrar'
    //       }).then(() => {
    //         this.popupOptions.visible = true;
    //         this.popupOptions.loading = false;
    //       });
    //     });
    //   }
      
import { inject } from '@angular/core';
import {  ApiResponse, ApiResponseData } from '../../../../@helpers/responses';
import { IParam } from '../../../../@helpers/services/base.service';
import { AppService } from '../../shared/services/app.service';

export class ConfigurationBaseService<TEntity> {
    private readonly _appService = inject(AppService);
    private prefix: string = "";

    constructor(url: string) {
        this.prefix = url;
    }

    get(withActive: boolean = false, active: boolean = false): Promise<ApiResponseData<TEntity[]>> {
        const params: Array<IParam> = new Array<IParam>();

        params.push({ type: 'params', name: 'withActive', value: withActive });
        params.push({ type: 'params', name: 'active', value: active });

        return this._appService.get(`${this.prefix}`);
    }

    add(entity: TEntity): Promise<string> {
        return this._appService.post(`${this.prefix}`, JSON.stringify(entity));
    }

    update(entity: TEntity): Promise<string> {
        return this._appService.put(`${this.prefix}`, JSON.stringify(entity));
    }

    updateState(id: number, estaActivo: boolean) {
        return this._appService.patch(`${this.prefix}/${id}`, JSON.stringify(estaActivo));
    }

    updateEstado(id: number, property: string, value: boolean): Promise<ApiResponse> {
        return this._appService.patch(`${this.prefix}/${id}?property=${property}&value=${value}`);
    }
}
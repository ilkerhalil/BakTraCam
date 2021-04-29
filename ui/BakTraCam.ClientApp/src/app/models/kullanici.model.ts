import { BasicBase } from './ortak.model';

export interface KullaniciModelBasic extends BasicBase {
    Name: string;
    Unvan: string;
}

export interface KullaniciModel extends BasicBase {
    Name: string;
    UnvanId: number;
}

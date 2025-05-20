import {atom} from 'jotai';
import {ActivityLogDto, GetServiceResponseDto, UserResponseDto} from '../models/generated-client.ts';

export const JwtAtom = atom<string>(localStorage.getItem('jwt') || '')

export const AdminAtom = atom<UserResponseDto>();

export const UsersAtom = atom<UserResponseDto[]>([]);

export const ServicesAtom = atom<GetServiceResponseDto[]>([]);

export const ActivityLogAtom = atom<ActivityLogDto[]>([]);

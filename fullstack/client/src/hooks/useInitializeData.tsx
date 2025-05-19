import {useAtom} from "jotai";
import {JwtAtom, ServicesAtom, UsersAtom} from "../atoms/atoms.ts";
import {useEffect} from "react";
import {serviceClient, userClient} from "../apiControllerClients.ts";

export default function useInitializeData() {
    const [users, setUsers] = useAtom(UsersAtom);
    const [services, setServices] = useAtom(ServicesAtom);
    const [jwt] = useAtom(JwtAtom);

    useEffect(() => {
        serviceClient.getAllServices().then(r => {
            setServices(r);
        })
    }, [setServices])

    useEffect(() => {
        userClient.getAllUsers(jwt).then(r => {
            setUsers(r);
        })
    }, [setUsers])
}
import {useAtom} from "jotai";
import {UsersAtom} from "../../atoms/atoms.ts";

export const Table = () => {
    const [users, setUsers] = useAtom(UsersAtom);

    return (
        <div className="overflow-x-auto">
            <table className="table table-xs">
                <thead>
                <tr>
                    <th></th>
                    <th>First name</th>
                    <th>Last name</th>
                    <th>Rfid</th>
                    <th>Created at</th>
                    <th>Updated at</th>
                </tr>
                </thead>
                <tbody>
                {users.map((user) => (
                    <tr key={user.id}>
                        <td></td>
                        <td>{user.firstName}</td>
                        <td>{user.lastName}</td>
                        <td>{user.rfid}</td>
                        <td>{user.createdAt}</td>
                        <td>{user.updatedAt}</td>
                    </tr>
                ))}

                </tbody>
            </table>
        </div>
    )
}

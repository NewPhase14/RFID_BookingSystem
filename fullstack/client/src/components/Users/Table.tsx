import {useAtom} from "jotai";
import {UsersAtom} from "../../atoms/atoms.ts";

export const Table = () => {
    const [users] = useAtom(UsersAtom);

    return (
        <div className="overflow-x-auto rounded-box border border-base-content/5 mt-4 mx-4">
            <table className="table">
                <thead>
                <tr>
                    <th>Email</th>
                    <th>First name</th>
                    <th>Last name</th>
                    <th>Rfid</th>
                    <th>Created at</th>
                    <th>Updated at</th>
                </tr>
                </thead>
                <tbody>
                {users.map((users) => (
                    <tr key={users.id}>
                        <td>{users.email}</td>
                        <td>{users.firstName}</td>
                        <td>{users.lastName}</td>
                        <td>{users.rfid}</td>
                        <td>{users.createdAt}</td>
                        <td>{users.updatedAt}</td>
                    </tr>
                ))}

                </tbody>
            </table>
        </div>
    )
}

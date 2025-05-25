import {useAtom} from "jotai";
import {JwtAtom, UsersAtom} from "../../atoms/atoms.ts";
import {FiTrash} from "react-icons/fi";
import {userClient} from "../../apiControllerClients.ts";
import toast from "react-hot-toast";

export const Table = () => {
    const [users, setUsers] = useAtom(UsersAtom);
    const [jwt] = useAtom(JwtAtom);

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
                    <th>Delete User</th>
                </tr>
                </thead>
                <tbody>
                {users.map((user) => (
                    <tr key={user.id}>
                        <td>{user.email}</td>
                        <td>{user.firstName}</td>
                        <td>{user.lastName}</td>
                        <td>{user.rfid}</td>
                        <td>{user.createdAt}</td>
                        <td>{user.updatedAt}</td>
                        <td><button onClick={() => { userClient.deleteUser(user.id, jwt).then(() => {
                            toast.success("User deleted successfully");
                            setUsers(users.filter((u) => u.id !== user.id));
                        })}} className="btn btn-sm bg-red-800"><FiTrash/></button></td>
                    </tr>
                ))}

                </tbody>
            </table>
        </div>
    )
}

import { Button } from "@/components/ui/button"
import {
  Card,
  CardContent,
  CardDescription,
  CardFooter,
  CardHeader,
  CardTitle,
} from "@/components/ui/card"
import { Input } from "@/components/ui/input"
import { Label } from "@/components/ui/label"
import {
  Tabs,
  TabsContent,
  TabsList,
  TabsTrigger,
} from "@/components/ui/tabs"
import Order from "./(order)"
import Payment from "./(payment)"
import Stock from "./(stock)"
import Delivery from "./(delivery)"

export default function Home() {
  return (
    <Tabs defaultValue="account" className="w-[400px]">
      <TabsList className="grid w-full grid-cols-4">
        <TabsTrigger value="order">Pedido</TabsTrigger>
        <TabsTrigger value="payment">Pagamento</TabsTrigger>
        <TabsTrigger value="stock">Estoque</TabsTrigger>
        <TabsTrigger value="delivery">Entrega</TabsTrigger>
      </TabsList>
      <TabsContent value="order">
        <Card>
          <CardHeader>
            <CardTitle>Pedido</CardTitle>
            <CardDescription>
              Detalhes sobre o pedido
            </CardDescription>
          </CardHeader>
          <CardContent className="space-y-2">
            <Order />
          </CardContent>
        </Card>
      </TabsContent>
      <TabsContent value="payment">
        <Card>
          <CardHeader>
            <CardTitle>Pagamento</CardTitle>
            <CardDescription>
              Detalhes sobre o pagamento
            </CardDescription>
          </CardHeader>
          <CardContent className="space-y-2">
            <Payment />
          </CardContent>
        </Card>
      </TabsContent>
      <TabsContent value="stock">
        <Card>
          <CardHeader>
            <CardTitle>Estoque</CardTitle>
            <CardDescription>
              Detalhes sobre o estoque
            </CardDescription>
          </CardHeader>
          <CardContent className="space-y-2">
            <Stock />
          </CardContent>
        </Card>
      </TabsContent>
      <TabsContent value="delivery">
        <Card>
          <CardHeader>
            <CardTitle>Entrega</CardTitle>
            <CardDescription>
              Detalhes sobre a entrega
            </CardDescription>
          </CardHeader>
          <CardContent className="space-y-2">
            <Delivery />
          </CardContent>
        </Card>
      </TabsContent>
    </Tabs>
  )
}
